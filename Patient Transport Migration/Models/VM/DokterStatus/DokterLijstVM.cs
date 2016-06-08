using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration.Models.VM.DokterStatus {
    public class DokterLijstVM {
        public DokterLijstVM(IEnumerable<Dokter> DoktersExclusiefRadiologie) {
            _dokterLijst = DoktersExclusiefRadiologie;
        }

        [Display(Name = "Selecteer een dokter")]
        public string DokterLijstSelected { get; set; }
        private IEnumerable<Dokter> _dokterLijst;
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