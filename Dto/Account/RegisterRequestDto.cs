using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.Dto.Account
{
    public class RegisterRequestDto
    {
        [Required]
        public string? UserName { get; set; }  
        [Required]
        [EmailAddress]
        public string? Email { get; set; } 
        [Required]
        public string Password { get; set;} = string.Empty;
    }
}