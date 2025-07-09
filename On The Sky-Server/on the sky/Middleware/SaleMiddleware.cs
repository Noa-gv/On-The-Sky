namespace on_the_sky.Middleware
{
    public class SaleMiddleware
    {
        private readonly RequestDelegate _next;
        public SaleMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("..");
            DateTime requestTime = DateTime.Now;
            var requestMethod = context.Request.Method;
          

            //if (requestMethod == "POST")
            //{
               if (requestTime.Hour >= 1 && requestTime.Hour < 6 && !context.Response.HasStarted)
               {
                 Console.WriteLine("succes2");
                 await context.Response.WriteAsync($"The site is under development 🏗️");
                 return;
               }
            //}

            await _next(context);
        }
    }
}