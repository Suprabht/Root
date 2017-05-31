using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Home;

namespace WebApp.ViewComponents
{
    public class UserDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(UserDetails userDetails)
        {
            //UserDetails userDetails = new UserDetails
            //{
            //    Name = "Suprabhat Paul",
            //    Address = "Address Road",
            //    Phone = 111111,
            //    AlternateEmail = "alternet@sdl.co",
            //    AlternetPhone = 12344,
            //    Email = "Email@sdl.co",
            //    BloodGroup = "O+ve"

            //};

            return await Task.Run(() => View(userDetails));
        }
    }
}
