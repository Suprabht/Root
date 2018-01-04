using System;
using System.Collections.Generic;

namespace Dal.Models.Identity
{
    public partial class ProgramCategory
    {
        public ProgramCategory()
        {
            Program = new HashSet<Program>();
        }

        public int ProgramCategoryId { get; set; }
        public string ProgramCategoryCode { get; set; }
        public string ProgramCategoryAbbreviation { get; set; }
        public string ProgramCategoryName { get; set; }
        public string ProgramCategoryDescription { get; set; }

        public virtual ICollection<Program> Program { get; set; }
    }
}
