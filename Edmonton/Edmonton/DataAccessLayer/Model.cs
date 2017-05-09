using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class BridgeToCareContext: DbContext
    {
        public DbSet<AspNetUser> AspNetUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=BLRBAPSPAUL02;Database=BridgeToCare;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True");
        }
    }

    public class AspNetUser
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
    }

}
