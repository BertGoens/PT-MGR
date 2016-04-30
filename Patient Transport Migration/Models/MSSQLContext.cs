using System;
using System.Collections.Generic;
using System.Data.Entity;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models {
    public class MSSQLContext : DbContext {

        public MSSQLContext() 
            : base("MSSQLDB") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MSSQLContext, Migrations.Configuration >());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            //base.OnModelCreating(modelBuilder);
            
        }

        public DbSet<ExceptionLogger> ExceptionLoggers { get; set; }

        public DbSet<Dokter> Docters { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<TransportTask> TransportTasks { get; set; }

        public System.Data.Entity.DbSet<Patient_Transport_Migration.Models.DAL.RequestStatus> RequestStatus { get; set; }

        public System.Data.Entity.DbSet<Patient_Transport_Migration.Models.DAL.RequestType> RequestTypes { get; set; }

        //public DbSet<DiagnostischeOnderzoeken> DiagnostischeOnderzoeken { get; set; }

        //public DbSet<RequestData> RequestData { get; set; }

        //public DbSet<RequestType> RequestTypes { get; set; }

        //public DbSet<RequestStatus> RequestStatus { get; set; }
    }
}