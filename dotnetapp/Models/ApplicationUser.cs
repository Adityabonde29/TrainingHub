using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace dotnetapp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Name {get; set;}
        public int UserId { get; set; } 
        
    }
}