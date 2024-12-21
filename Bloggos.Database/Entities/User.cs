using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.Database.Entities
{
    public class User
    {
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Password hash must be exactly 64 characters.")]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "Password salt must be exactly 16 characters.")]
        public string PasswordSalt { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
