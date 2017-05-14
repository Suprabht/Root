using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels.Account
{
    public class ForgetPasswordViewModel
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
