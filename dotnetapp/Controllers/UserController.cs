using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnetapp.Models;
using dotnetapp.Data;
using Microsoft.AspNetCore.Identity;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                MobileNumber = user.MobileNumber,
                Email = user.Email
            };

            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(string id, [FromBody] UserDto userUpdateDto)
        {
            if (id == null || userUpdateDto == null)
            {
                return BadRequest(new { message = "Invalid user ID or update data." });
            }

            var user = await _context.Users.FindAsync(int.Parse(id));
            if (user == null)
            {
                return NotFound(new { message = "User not found in custom table." });
            }

            var aspEmail = user.Email;
            user.Email = userUpdateDto.Email;
            user.Username = userUpdateDto.Username;
            user.MobileNumber = userUpdateDto.MobileNumber;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var aspNetUser = await _userManager.FindByEmailAsync(aspEmail);
            if (aspNetUser == null)
            {
                return NotFound(new { message = "User not found." });
            }

            aspNetUser.Email = userUpdateDto.Email;
            aspNetUser.UserName = userUpdateDto.Username;
            var result = await _userManager.UpdateAsync(aspNetUser);
            if (!result.Succeeded)
            {
                return StatusCode(500, new { message = "Failed to update AspNetUser." });
            }

            return Ok(new { message = "User profile updated successfully." });
        }



    }
}