using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TGAcademyMVC.Models;

namespace TGAcademyMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Checklist> Checklists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Checklist>().ToTable("Checklists");
            //builder.Entity<ApplicationUser>(b =>
            //{
            //    b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            //});

            //builder.Entity<IdentityRole>(b =>
            //{
            //    b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            //});
        }
    }
}
