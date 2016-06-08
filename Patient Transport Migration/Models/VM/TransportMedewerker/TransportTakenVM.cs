using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration.Models.VM.TransportMedewerker {
    public class TransportTakenVM {
        public TransportTakenVM(Context context, string medewerkerId) {
            TransportTaakLijst = new TransportTaakRepository(context).GetWerknemerTakenQueue(medewerkerId).ToList();
        }

        public List<TransportTaak> TransportTaakLijst { get; private set; }
    }
}