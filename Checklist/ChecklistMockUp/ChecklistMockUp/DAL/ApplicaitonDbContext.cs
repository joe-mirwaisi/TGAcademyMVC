using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

using ChecklistMockUp.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ChecklistMockUp.DAL
{

    public class ApplicaitonDbContext : DbContext
    {
        public ApplicaitonDbContext() : base("ApplicaitonDbContext")
        {
        }

        public DbSet<Widget> Widgets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}