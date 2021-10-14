using System;
using System.ComponentModel.DataAnnotations;

namespace DailyVisitors.WebApi.Authentication
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
    }
}
