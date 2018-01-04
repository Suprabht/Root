using System;
using System.Collections.Generic;

namespace Dal.Models.Identity
{
    public partial class ClientDetails
    {
        public ClientDetails()
        {
            Assignment = new HashSet<Assignment>();
        }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string Long { get; set; }
        public string Latt { get; set; }
        public int ProgramId { get; set; }

        public virtual ICollection<Assignment> Assignment { get; set; }
        public virtual Program Program { get; set; }
    }
}
