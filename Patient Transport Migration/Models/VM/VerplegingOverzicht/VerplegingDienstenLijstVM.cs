using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.Model;

namespace Patient_Transport_Migration.Models.VM.VerplegingOverzicht {
    public class VerplegingDienstenLijstVM {
        public VerplegingDienstenLijstVM(string Afdeling) {
            var db = new Context();
            _afdelingLijst = db.tblLocaties.Select(l =>
                    new Afdeling() { Omschrijving = l.Omschrijving, Code = l.Afdeling })
                    .Distinct()
                    .ToList();
            AfdelingSelected = Afdeling;
        }

        [Display(Name = "Afdeling")]
        public string AfdelingSelected { get; set; }
        private List<Afdeling> _afdelingLijst { get; set; }
        public IEnumerable<SelectListItem> AfdelingLijst {
            get {
                var result = _afdelingLijst.Select(a => new SelectListItem {
                    Text = a.Omschrijving,
                    Value = a.Code
                });
                return result;
            }
        }

    }
}