using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    public class PatientDetailsVM {
        public PatientDetailsVM() { }

        public PatientDetailsVM(string visitId) {
            PatientVisitId = visitId;
            if (!string.IsNullOrEmpty(visitId)) {
                var db = new MSSQLContext();
                try {
                    PatientDetails = db.tblPatienten.Where(p => p.PatientVisit.Equals(visitId)).First();
                } catch (Exception ex) {
                    ex.ToString();
                }
            }

        }

        public string PatientVisitId { get; private set; }
        public Patient PatientDetails { get; private set; }
    }
}