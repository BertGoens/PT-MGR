using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using Patient_Transport_Migration.Models;

namespace Patient_Transport_Migration.Migrations {
    public class Configuration : DbMigrationsConfiguration<Models.MSSQLContext> {

        public Configuration() {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MSSQLContext context) {
            base.Seed(context);
        }
    }
}