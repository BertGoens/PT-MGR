using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.VM.DispatchOverzicht {
    public class WerknemersVM {
        public WerknemersVM(string TakenPage) {
            var db = new Context();
            TransportWerknemers = db.tblTransportWerknemers.ToList();
        }

        public List<TransportWerknemer> TransportWerknemers { get; private set; }
    }
}