using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using dotnetapp.Data;
using dotnetapp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
 
namespace dotnetapp.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
 
        public AuthService(UserManager<ApplicationUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IConfiguration configuration,
                           ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }
 
        public async Task<(int, string)> Registration(User model, string role)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return (0, "User details cannot be null or empty.");
            }
 
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return (0, "User already exists");
 
            ApplicationUser identityUser = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Username,
                PhoneNumber = model.MobileNumber,
                Name = model.Username
            };
 
            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (!result.Succeeded)
                return (0, "User creation failed! Please check user details and try again");
 
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));
 
            await _userManager.AddToRoleAsync(identityUser, role);
 
            // Save user to Users DbSet
            var newUser = new User
            {
                Email = model.Email,
                Username = model.Username,
                MobileNumber = model.MobileNumber,
                UserRole = role,
                Password = model.Password
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
 
            return (1, "User created successfully!");
        }
 
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }
 
        // public async Task<(int, string)> Login(LoginModel model)
        // {
        //     if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        //     {
        //         return (0, "Email and password cannot be null or empty.");
        //     }
 
        //     var user = await _userManager.FindByEmailAsync(model.Email);
        //     if (user == null)
        //         return (0, "Invalid email");
 
        //     if (!await _userManager.CheckPasswordAsync(user, model.Password))
        //         return (0, "Invalid password");
 
        //     var userRoles = await _userManager.GetRolesAsync(user);
        //     var authClaims = new List<Claim>
        // {
        //     new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        // };
 
        //     foreach (var userRole in userRoles)
        //     {
        //         authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //     }
 
        //     var token = GenerateToken(authClaims);
 
        //     return (1, token);
        // }
 
 
        public async Task<(int, string)> Login(LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return (0, "Email and password cannot be null or empty.");
            }
 
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (0, "Invalid email");
 
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return (0, "Invalid password");
 
            // Retrieve user ID from the database
            var userId = await GetUserIdFromDatabase(user.Email);
            if (userId == null)
                return (0, "User ID not found");
 
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserID", userId) // Add user ID to the claims
            };
 
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
 
            var token = GenerateToken(authClaims);
 
            return (1, token);
        }
 
        // Method to retrieve user ID from the database
        private async Task<string> GetUserIdFromDatabase(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user?.UserId.ToString();
        }
 
 
 
private string GenerateToken(IEnumerable<Claim> claims)
{
    var secret = _configuration["JWT:Secret"];
    if (string.IsNullOrEmpty(secret))
    {
        throw new ArgumentNullException("JWT:Secret", "JWT:Secret configuration value is null or empty.");
    }
 
    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    var token = new JwtSecurityToken(
        issuer: _configuration["JWT:ValidIssuer"],
        audience: _configuration["JWT:ValidAudience"],
        expires: DateTime.Now.AddHours(3),
        claims: claims,
        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
    );
 
    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
    Console.WriteLine("Generated Token: " + tokenString); // Log the generated token
    return tokenString;
}
public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}