using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Model;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration.Models.VM.VerplegingOverzicht {
    public class VerplegingDienstenLijstVM {
        public VerplegingDienstenLijstVM(Context context, string Afdeling) {
            _afdelingLijst = new LocatieRepository(context).GetUniekeAfdelingen();
            AfdelingSelected = Afdeling;
        }

        [Display(Name = "Patient-Afdeling")]
        public string AfdelingSelected { get; set; }
        private IEnumerable<Afdeling> _afdelingLijst { get; set; }
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