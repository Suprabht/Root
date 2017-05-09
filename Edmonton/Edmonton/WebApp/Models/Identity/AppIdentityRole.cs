using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Identity
{
    public class AppIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
