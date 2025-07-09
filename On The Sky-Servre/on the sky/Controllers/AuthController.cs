using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using on_the_sky.core.entities;
using on_the_sky.core.services;
using on_the_sky.models;

namespace on_the_sky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly iUsersService _usersService;
        private readonly IConfiguration _configuration;

        public AuthController(iUsersService usersService, IConfiguration configuration)
        {
            _usersService = usersService;
            _configuration = configuration;
        }

        // GET api/auth/me
        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userName = User.Identity?.Name;
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userName == null || role == null || userId == null)
                return Unauthorized();

            return Ok(new
            {
                userName,
                role,
                id = int.Parse(userId)
            });
        }

        // POST api/auth
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _usersService.GetByName(loginModel.UserName, loginModel.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role == 0 ? "manager" : "passanger"),
                    new Claim("UserId", user.Id.ToString()) // ← מזהה משתמש
                };

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // בדיקה אם המשתמש כבר קיים לפי שם בלבד
            var isExist = (await _usersService.Getlist())
                            .Any(u => u.UserName.ToLower() == model.UserName.ToLower());
            if (isExist)
                return BadRequest("Username already exists");

            var user = new Users
            {
                UserName = model.UserName,
                Password = model.Password,
                Role = Users.eRole.passanger  // ← תמיד משתמש רגיל
            };

            await _usersService.ADD(user);

            return Ok("User registered successfully");
        }

    }
}
