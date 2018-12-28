using System;
using System.Collections.Generic;
using System.Text;
using grow.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace grow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Plant> Plant { get; set; }
        public DbSet<Light> Light { get; set; }
        public DbSet<PlantAudit> PlantAudit { get; set; }
        public DbSet<PlantType> PlantType { get; set; }
        public DbSet<Water> Water { get; set; }
        public string WebRootPath { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlantAudit>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Plant>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
