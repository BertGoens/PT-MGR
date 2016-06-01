using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.DokterStatus {
    public class DokterLijstVM {
        public DokterLijstVM() {
            var db = new MSSQLContext();
            _dokterLijst = db.tblDokters.ToList();
        }

        [Display(Name = "Selecteer een dokter")]
        public string DokterLijstSelected { get; set; }
        private List<Dokter> _dokterLijst;
        public IEnumerable<SelectListItem> DokterLijst {
            get {
                return _dokterLijst.Select(d => new SelectListItem {
                    Text = d.Naam,
                    Value = d.Id.ToString()
                });
            }
        }
    }
}