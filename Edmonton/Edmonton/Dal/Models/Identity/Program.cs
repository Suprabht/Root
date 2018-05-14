using System;
using System.Collections.Generic;

namespace Dal.Models.Identity
{
    public partial class Program
    {
        public Program()
        {
            ClientDetailsPrograms = new HashSet<ClientDetailsPrograms>();
        }

        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public int ProgramCategoryId { get; set; }

        public virtual ICollection<ClientDetailsPrograms> ClientDetailsPrograms { get; set; }
        public virtual ProgramCategory ProgramCategory { get; set; }
    }
}
