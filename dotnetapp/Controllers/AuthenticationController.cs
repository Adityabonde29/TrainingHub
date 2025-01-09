using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly EmailService _emailService;

        private  IConfiguration _configuration;

        public AuthenticationController(IAuthService authService, EmailService emailService, IConfiguration configuration)
        {
            _authService = authService;
            _emailService = emailService;
            _configuration = configuration;

        }




        // [ValidateAntiForgeryToken]
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _authService.Login(model);

                if (result.Item1 == 0)
                    return Unauthorized(result.Item2);

                var subject = "🚀 Welcome Back to Training Hub!";
                var message = "Welcome back to Trainin Hub! We're thrilled to see you again. Dive right in and continue your fitness journey with us. Remember, every step counts! Keep pushing your limits and stay fit!";
                await _emailService.SendEmailAsync(model.Email, subject, message);

                return Ok(new { Token = result.Item2 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // [ValidateAntiForgeryToken]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _authService.Registration(model, model.UserRole);
                if (result.Item1 == 0)
                {
                    return BadRequest(result.Item2);
                }
                var subject = "🎉 Welcome to Training Hub! Your Fitness Journey Starts Now!";
                var message = "Welcome to Trainin Hub! We're excited to have you join our community. Get ready to embark on a journey towards better health and fitness. Let's achieve your goals together! Stay motivated and keep pushing your limits!";
                await _emailService.SendEmailAsync(model.Email, subject, message);


                return Ok(new { message = result.Item2 });

                // return Ok();
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        
        }
        
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _authService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    

    [HttpPost("verify-secret-key")]
    public IActionResult VerifySecretKey([FromBody] VerifySecretKeyRequest request)
    {
        var secretKey = _configuration["AdminSecretKey"];
        if (request.SecretKey == secretKey)
        {
            return Ok(true);
        }
        return Unauthorized("Invalid secret key.");
    }

    
public class VerifySecretKeyRequest
{
    public string SecretKey { get; set; }
}

    
        
    }

}


