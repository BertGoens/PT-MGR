using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Util;

namespace Patient_Transport_Migration.Models.VM.VerplegingOverzicht {
    public class RecentVervoerdePatienten {
        public RecentVervoerdePatienten(Context context, string page, string afdelingCode) {

            // Query alle patienten met recentelijke vervoertaken die in orde zijn
            DateTime date = DateTime.Now.AddHours(-4);
            var _qryItems = context.tblTransportTaken.Where(t =>
                t.Aanvraag.AanvraagType.Include_Patient // Bevat patient
                && t.DatumCompleet > date // Is recent
                && t.TransportWerknemerId != null) // Als = null dan is het verwijderd (geen echte taak)
                .ToList();
            _qryItems.Reverse();

            if (!string.IsNullOrEmpty(afdelingCode)) { //Filter als een afdeling gegeven is (op de code)
                _qryItems = _qryItems.Where(t => 
                t.Aanvraag.Patient.Afdeling == afdelingCode)
                .ToList();
            }

            int pageNr = 0;
            int.TryParse(page, out pageNr);
            Pager = new Pager(_qryItems.Count(), pageNr);
            PatientVervoerTakenInOrde = _qryItems.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();
        }

        public List<TransportTaak> PatientVervoerTakenInOrde { get; private set; }

        public Pager Pager { get; private set; }
    }
}