﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Home
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string Long { get; set; }
        public string Latt { get; set; }
        public string Link { get; set; }
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public int ProgramCategoryId { get; set; }
        public string ProgramCategoryCode { get; set; }
        public string ProgramCategoryAbbreviation { get; set; }
        public string ProgramCategoryName { get; set; }
    }
}
