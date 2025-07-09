using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using on_the_sky.core;
using on_the_sky.core.repositories;
using on_the_sky.core.services;
using on_the_sky.Data.repositories;
using on_the_sky.service;
using System.Text;
using System.Text.Json.Serialization;

namespace on_the_sky
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ? CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularClient", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<IFlightService, FlightServicecs>();
            builder.Services.AddScoped<IFlightRepository, FlightRepositories>();
            builder.Services.AddScoped<IplaceService, PlaceServicecs>();
            builder.Services.AddScoped<IplaceRepository, PlaceRepositories>();
            builder.Services.AddScoped<ItravelService, TravelService>();
            builder.Services.AddScoped<ITravelRepository, TravelRepository>();
            builder.Services.AddScoped<iUsersService, UsersServicecs>();
            builder.Services.AddScoped<iUsersRipository, UsersRipositories>();

            builder.Services.AddDbContext<Datacontext>();
            builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(MappingPostModel));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            var app = builder.Build();

            // ? Use CORS
            app.UseCors("AllowAngularClient");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Datacontext>();
                var users = db.UsersDB.ToList();
                Console.WriteLine(" משתמשים ב-DB:");
                foreach (var u in users)
                {
                    Console.WriteLine($" {u.UserName}, {u.Password}, {u.Role}");
                }
            }

            app.Run();
        }
    }
}
