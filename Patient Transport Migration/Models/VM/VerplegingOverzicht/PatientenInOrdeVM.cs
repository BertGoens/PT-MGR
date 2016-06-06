using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Util;

namespace Patient_Transport_Migration.Models.VM.VerplegingOverzicht {
    public class RecentVervoerdePatienten {
        public RecentVervoerdePatienten(string page) {
            var db = new MSSQLContext();

            // Query alle patienten met recentelijke vervoertaken die in orde zijn
            DateTime date = DateTime.Now.AddHours(-4);
            var _qryItems = db.tblTransportTaken.Where(t =>
                t.Aanvraag.AanvraagType.Include_Patient // Bevat patient
                && t.DatumCompleet > date // Is recent
                && t.TransportWerknemerId != null) // Als = null dan is het verwijderd (geen echte taak)
                .ToList();
            _qryItems.Reverse();

            int pageNr = 0;
            int.TryParse(page, out pageNr);
            Pager = new Pager(_qryItems.Count(), pageNr);
            PatientVervoerTakenInOrde = _qryItems.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();
        }

        //  TODO Constructor met filter voor dienst?

        public List<TransportTaak> PatientVervoerTakenInOrde { get; private set; }

        public Pager Pager { get; private set; }
    }
}