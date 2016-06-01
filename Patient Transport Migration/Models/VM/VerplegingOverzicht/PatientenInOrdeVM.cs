using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.VerplegingOverzicht {
    public class PatientenInOrdeVM {
        public PatientenInOrdeVM() {
            var db = new MSSQLContext();

            PatientVervoerTakenInOrde = db.tblTransportTaken.Where(t =>
                // Bevat patient
                !string.IsNullOrEmpty(t.Aanvraag.PatientId) || !string.IsNullOrEmpty(t.Aanvraag.PatientVisit) &&
                // Is ongoing
                t.DatumCompleet != null)
                .ToList();
        }

        //  TODO Constructor met filter voor dienst
        //  TODO Paginering?

        /// <summary>
        /// Lijst van <c>TransportTaak</c> met een <c>Patient</c> 
        /// die recentelijk verplaatst zijn door Transport personeel
        /// </summary>
        public List<TransportTaak> PatientVervoerTakenInOrde { get; private set; }
    }
}