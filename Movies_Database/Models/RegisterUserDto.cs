using System.ComponentModel.DataAnnotations;

namespace Movies_Database.Models
{
    public class RegisterUserDto
    {
        
        public string Email { get; set; }

        
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        
        public string Username { get; set; }

    }
}

