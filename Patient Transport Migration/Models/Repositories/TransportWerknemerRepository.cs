using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.Repositories {
    public class TransportWerknemerRepository {
        private Context db;

        public TransportWerknemerRepository() {
            db = new Context();
        }

        public IEnumerable<TransportWerknemer> GetAll() {
            return db.tblTransportWerknemers.ToList();
        }
    }
}