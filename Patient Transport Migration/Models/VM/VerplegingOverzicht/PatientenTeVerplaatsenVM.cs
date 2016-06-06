using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Util;

namespace Patient_Transport_Migration.Models.VM.VerplegingOverzicht {
    public class PatientenTeVerplaatsenVM {
        public PatientenTeVerplaatsenVM(string page) {
            var db = new MSSQLContext();

            // query de recente uitgevoerde taken met een patient

            DateTime date = DateTime.Now.AddHours(-4);
            var _qryItems = db.tblTransportTaken
                .Where(t =>
                t.Aanvraag.PatientId != null
                && t.DatumCompleet == null)
                .OrderBy(t => t.DatumGemaakt)
                .ToList();
            _qryItems.Reverse(); //Nieuwste (datum) bovenaan

            int pageNr = 0;
            int.TryParse(page, out pageNr);
            Pager = new Pager(_qryItems.Count(), pageNr);
            PatientenWachtend = _qryItems.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();
        }

        // TODO Filter maken per dienst

        public List<TransportTaak> PatientenWachtend { get; private set; }

        public Pager Pager { get; private set; }
    }
}