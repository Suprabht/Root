using System;
using DailyVisitors.EnumConstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DailyVisitors.DAL.Models
{
    public partial class VisitorsBookContext : DbContext
    {
        private string ConnectionString;
        public VisitorsBookContext()
        {
        }
        public VisitorsBookContext(string connectionString) => ConnectionString = connectionString;
        public VisitorsBookContext(DbContextOptions<VisitorsBookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VisitorDetails> VisitorDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DateOfJoining).HasColumnType("datetime");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(500);

                entity.Property(e => e.IdentityName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.InsertDateTime).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(500);
            });

			modelBuilder.Entity<VisitorDetails>(entity =>
			{
				entity.HasKey(e => e.VisitorId);

				entity.Property(e => e.Email).IsRequired();

				entity.Property(e => e.LoginDateTime).HasColumnType("datetime");

				entity.Property(e => e.LogoutDateTime).HasColumnType("datetime");

				entity.Property(e => e.VisitorName).IsRequired();

				entity.HasOne(d => d.User)
					.WithMany(p => p.VisitorDetails)
					.HasForeignKey(d => d.UserId)
					.HasConstraintName("FK_VisitorDetails_Users");
			});
		}
    }
}
