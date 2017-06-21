using System;
using System.Collections.Generic;

namespace Dal.Models.Identity
{
    public partial class ClientDetails
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string Long { get; set; }
        public string Latt { get; set; }
    }
}
