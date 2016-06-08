using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.DokterStatus {
    public class DokterDetailsVM {
        public DokterDetailsVM() {
            //REQUIRED FOR POST
        }

        public DokterDetailsVM(string dokterDetailsId) {
            var db = new DokterContext();
            try {
                DokterDetails = db.tblDokters.First(d => d.Id.Equals(dokterDetailsId));
                Id = DokterDetails.Id;
                IsConsultVerwachtend = DokterDetails.IsConsultVerwachtend;
            } catch (Exception) {
            }
        }

        public Dokter DokterDetails { get; private set; }

        public string Id { get; set; }

        [Display(Name = "Verwacht Consult")]
        public bool IsConsultVerwachtend { get; set; }

    }
}