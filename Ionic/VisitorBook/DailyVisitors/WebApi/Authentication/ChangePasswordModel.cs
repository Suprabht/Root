using System;
using System.ComponentModel.DataAnnotations;

namespace DailyVisitors.WebApi.Authentication
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "User email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
    }
}
