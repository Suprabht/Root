using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Home
{
    public class Program
    {
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public int ProgramCategoryId { get; set; }
        public string ProgramCategoryCode { get; set; }
        public string ProgramCategoryAbbreviation { get; set; }
        public string ProgramCategoryName { get; set; }
    }
}
