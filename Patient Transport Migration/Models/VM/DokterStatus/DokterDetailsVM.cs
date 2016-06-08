using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration.Models.VM.DokterStatus {
    public class DokterDetailsVM {
        public DokterDetailsVM() {
            //REQUIRED FOR POST
        }

        public DokterDetailsVM(Dokter dokterDetails) {
            Id = dokterDetails.Id;
            IsConsultVerwachtend = dokterDetails.IsConsultVerwachtend;
            this.DokterDetails = dokterDetails;
        }

        public Dokter DokterDetails { get; private set; }

        public string Id { get; set; }

        [Display(Name = "Verwacht Consult")]
        public bool IsConsultVerwachtend { get; set; }

    }
}