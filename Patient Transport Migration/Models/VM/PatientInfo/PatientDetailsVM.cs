using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    public class PatientDetailsVM {
        public PatientDetailsVM() { }

        public PatientDetailsVM(string visitId, Context context) {
            PatientVisitId = visitId;
            if (!string.IsNullOrEmpty(visitId)) {
                var db = new Context();
                try {
                    PatientDetails = new PatientRepository(context).GetPatientByVisitId(visitId);
                } catch (Exception ex) {
                    ex.ToString();
                }
            }

        }

        public string PatientVisitId { get; private set; }
        public Patient PatientDetails { get; private set; }
    }
}