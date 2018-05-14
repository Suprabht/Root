using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Home
{
    public class ClientDetailsPrograms
    {
        public int ClientDetailsProgramsId { get; set; }
        public int ClientId { get; set; }
        public int ProgramId { get; set; }

        public string ClientName { get; set; }
        public string ProgramName { get; set; }
    }
}
