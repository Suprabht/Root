using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Models.Identity
{
    public partial class ProgramDetails
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
    }
}
