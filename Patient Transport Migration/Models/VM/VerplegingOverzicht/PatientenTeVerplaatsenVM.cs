using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Util;

namespace Patient_Transport_Migration.Models.VM.VerplegingOverzicht {
    public class PatientenTeVerplaatsenVM {
        public PatientenTeVerplaatsenVM(string page, string afdeling) {
            var db = new MSSQLContext();

            // query de recente uitgevoerde taken met een patienten in afdeling

            DateTime date = DateTime.Now.AddHours(-4);
            var _qryItems = db.tblTransportTaken
                .Where(t =>
                t.Aanvraag.PatientId != null
                && t.DatumCompleet == null)
                .OrderBy(t => t.DatumGemaakt)
                .ToList();
            _qryItems.Reverse(); //Nieuwste (datum) bovenaan

            //Filter als een afdeling gegeven is (op de code)
            if (!string.IsNullOrEmpty(afdeling)) {
                _qryItems = _qryItems.Where(a => 
                a.Aanvraag.Patient.Afdeling == afdeling)
                .ToList();
            }

            int pageNr = 0;
            int.TryParse(page, out pageNr);
            Pager = new Pager(_qryItems.Count(), pageNr);
            PatientenWachtend = _qryItems.Skip((Pager.CurrentPage - 1) * Pager.PageSize)
                .Take(Pager.PageSize)
                .ToList();
        }

        public List<TransportTaak> PatientenWachtend { get; private set; }

        public Pager Pager { get; private set; }
    }
}