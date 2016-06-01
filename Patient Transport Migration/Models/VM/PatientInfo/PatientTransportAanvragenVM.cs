using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    public class PatientTransportAanvragenVM {
        public PatientTransportAanvragenVM() { }

        public PatientTransportAanvragenVM(string patientVisitId) {
            PatientVisitId = patientVisitId;

            if (!string.IsNullOrEmpty(patientVisitId)) {
                var db = new MSSQLContext();
                // Zoek alle medische recente aanvragen voor de patient
                //(recent doordat we filteren op patientvisit)
                PatientAanvragen = db.tblTransportTaken
                    .Where(a => a.Aanvraag.PatientVisit.Equals(patientVisitId))
                    .OrderBy(t => t.DatumGemaakt)
                    .ToList();
            }
        }

        public string PatientVisitId { get; private set; }

        /// <summary>
        /// Lijst van medische <c>Aanvraag</c> van ... voor deze patient
        /// </summary>
        public List<TransportTaak> PatientAanvragen { get; private set; }
    }
}