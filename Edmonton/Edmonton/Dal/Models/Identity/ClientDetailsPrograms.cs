using System;
using System.Collections.Generic;

namespace Dal.Models.Identity
{
    public partial class ClientDetailsPrograms
    {
        public int ClientDetailsProgramsId { get; set; }
        public int ClientId { get; set; }
        public int ProgramId { get; set; }

        public virtual ClientDetails Client { get; set; }
        public virtual Program Program { get; set; }
    }
}
