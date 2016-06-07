using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.TransportMedewerker {
    public class TransportTakenVM {
        public TransportTakenVM(string medewerkerId) {
            var db = new MSSQLContext();
            TransportTaakLijst = db.tblTransportTaken.Where(t =>
            t.TransportWerknemerId == medewerkerId)
            .ToList();
        }

        public List<TransportTaak> TransportTaakLijst;
    }
}