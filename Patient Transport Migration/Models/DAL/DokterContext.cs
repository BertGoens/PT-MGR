using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Patient_Transport_Migration.Models.DAL {
    public class DokterContext : DbContext {
        public DokterContext()
            : base("MSSQLDB_V3") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dokter> tblDokters { get; set; }
    }
}