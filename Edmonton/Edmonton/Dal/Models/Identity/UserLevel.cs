using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Models.Identity
{
    public partial class UserLevel
    {
        public UserLevel()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int UserLevelId { get; set; }
        public string UserLevelName { get; set; }
        public string UserLevelDescription { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
