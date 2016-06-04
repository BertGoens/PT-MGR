using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Util;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    public class PatientTransportAanvragenVM {
        public PatientTransportAanvragenVM() { }

        public PatientTransportAanvragenVM(string visitId, string page) {
            PatientVisitId = visitId;

            if (!string.IsNullOrEmpty(visitId)) {
                var db = new MSSQLContext();
                // Zoek alle medische recente aanvragen voor de patient
                //(recent doordat we filteren op patientvisit)
                var _qryItems = db.tblTransportTaken
                    .Where(a => a.Aanvraag.PatientVisit.Equals(visitId))
                    .OrderBy(t => t.DatumGemaakt)
                    .ToList();

                int pageNr = 0;
                int.TryParse(page, out pageNr);
                Pager = new Pager(_qryItems.Count(), pageNr);
                PatientAanvragen = _qryItems.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();
            }
        }

        public string PatientVisitId { get; private set; }

        /// <summary>
        /// Lijst van medische <c>Aanvraag</c> van ... voor deze patient
        /// </summary>
        public List<TransportTaak> PatientAanvragen { get; private set; }

        public Pager Pager { get; private set; }
    }
}