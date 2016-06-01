using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.VerplegingOverzicht {
    public class PatientenTeVerplaatsenVM {
        public PatientenTeVerplaatsenVM() {
            var db = new MSSQLContext();

            // query de recente uitgevoerde taken met een patient
            DateTime date = DateTime.Now.AddHours(-4);
            PatientenWachtend = db.tblTransportTaken
                .Where(t => !string.IsNullOrEmpty(t.Aanvraag.PatientId) && t.DatumCompleet < date)
                .OrderBy(t => t.DatumGemaakt)
                .ToList();

        }

        // TODO Filter maken per dienst

        /// <summary>
        /// Lijst van <c>TransportTaak</c> met een <c>Patient</c>
        /// die verplaatst zullen worden door Transport personeel
        /// </summary>
        public List<TransportTaak> PatientenWachtend { get; private set; }

    }
}