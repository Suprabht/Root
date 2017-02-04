using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Areas.SocialIntelligence.Models
{
    public class Person
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
