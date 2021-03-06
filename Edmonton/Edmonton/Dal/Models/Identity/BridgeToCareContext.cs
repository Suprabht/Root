﻿using System;
using Dal.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dal.Models.Identity
{
    public partial class BridgeToCareContext : DbContext
    {
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<ClientDetails> ClientDetails { get; set; }
        public virtual DbSet<ClientDetailsPrograms> ClientDetailsPrograms { get; set; }
        public virtual DbSet<EmergencyCall> EmergencyCall { get; set; }
        public virtual DbSet<Leave> Leave { get; set; }
        public virtual DbSet<Program> Program { get; set; }
        public virtual DbSet<ProgramCategory> ProgramCategory { get; set; }
        public virtual DbSet<ProgramDetails> ProgramDetails { get; set; }
        public virtual DbSet<Tracking> Tracking { get; set; }
        public virtual DbSet<UserLevel> UserLevel { get; set; }

        public BridgeToCareContext(DbContextOptions<BridgeToCareContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetUserRoles_RoleId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.BirthDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(256);
                entity.Property(e => e.SecondName).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
                entity.Property(e => e.AlternetEmail).HasMaxLength(256);
                entity.Property(e => e.Address).HasMaxLength(256);
                entity.Property(e => e.BloodGroup).HasMaxLength(256);
            });

            modelBuilder.Entity<ProgramDetails>(entity =>
            {
                entity.HasKey(e => e.ProgramId)
                    .HasName("PK_ProgramDetails");

                entity.Property(e => e.ProgramName).HasMaxLength(250);
            });
            
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.Property(e => e.AssignmentDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Assignment_ClientDetails");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Assignment_AspNetUsers");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasComputedColumnSql("getdate()")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Latt).HasMaxLength(50);

                entity.Property(e => e.Long).HasMaxLength(50);

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.AssignmentId)
                    .HasConstraintName("FK_Attendance_Assignment");
            });


            modelBuilder.Entity<ClientDetails>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                     .HasName("PK_ClientDetails");

                entity.Property(e => e.ClientAddress).IsRequired();

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Latt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Long)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ClientDetailsPrograms>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientDetailsPrograms)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ClientDetailsPrograms_ClientDetailsPrograms");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ClientDetailsPrograms)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ClientDetailsPrograms_Program");
            });

            modelBuilder.Entity<EmergencyCall>(entity =>
            {
                entity.Property(e => e.LoginTime).HasColumnType("smalldatetime");

                entity.Property(e => e.LogoutTime).HasColumnType("smalldatetime");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmergencyCall)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_EmergencyCall_AspNetUsers");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.Property(e => e.ProgramCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ProgramDescription).HasMaxLength(250);

                entity.Property(e => e.ProgramName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ProgramCategory)
                    .WithMany(p => p.Program)
                    .HasForeignKey(d => d.ProgramCategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Program_ProgramCategory");
            });

            modelBuilder.Entity<ProgramCategory>(entity =>
            {
                entity.Property(e => e.ProgramCategoryAbbreviation).HasMaxLength(5);

                entity.Property(e => e.ProgramCategoryCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ProgramCategoryDescription).HasMaxLength(250);

                entity.Property(e => e.ProgramCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProgramDetails>(entity =>
            {
                entity.HasKey(e => e.ProgramId)
                    .HasName("PK_ProgramDetails");

                entity.Property(e => e.ProgramName).HasMaxLength(250);
            });

            modelBuilder.Entity<Tracking>(entity =>
            {
                entity.Property(e => e.Latt).HasMaxLength(50);

                entity.Property(e => e.Long).HasMaxLength(50);

                entity.Property(e => e.Timestamp).HasColumnType("smalldatetime");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tracking)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tracking_AspNetUsers");
            });
            modelBuilder.Entity<UserLevel>(entity =>
            {
                entity.Property(e => e.UserLevelName).HasMaxLength(50);
            });
        }
    }
}