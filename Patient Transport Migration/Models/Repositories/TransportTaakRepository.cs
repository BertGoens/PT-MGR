using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.Repositories {
    public class TransportTaakRepository {
        private Context db;

        public TransportTaakRepository() {
            db = new Context();
        }

        public IEnumerable<TransportTaak> GetWachtendeTaken() {
            return db.tblTransportTaken.Where(t => 
            t.TransportWerknemer == null && 
            t.DatumCompleet == null);
        }
    }
}