using PooriaTicket2.Models;
using System.ComponentModel.DataAnnotations;

namespace PooriaTicket2.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public User? User { get; set; }

    }
}
