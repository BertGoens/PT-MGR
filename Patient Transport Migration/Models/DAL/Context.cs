using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.DAL {
    public class Context : DbContext {
        public Context() : base("MSSQLDB_V3") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>());           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Administrator> tblAdministrators { get; set; }

        public DbSet<DispatchWerknemer> tblDispatchWerknemers { get; set; }

        public DbSet<ExceptionLogger> tblExceptionLogger { get; set; }

        public DbSet<Dokter> tblDokters { get; set; }

        public DbSet<Patient> tblPatienten { get; set; }

        public DbSet<TransportTaak> tblTransportTaken { get; set; }

        public DbSet<TransportWerknemer> tblTransportWerknemers { get; set; }

        public DbSet<Aanvraag> tblAanvragen { get; set; }

        public DbSet<Transportwijze> tblTransportwijzes { get; set; }

        public DbSet<AanvraagType> tblAanvraagTypes { get; set; }

        public DbSet<Locatie> tblLocaties { get; set; }
    }
}
