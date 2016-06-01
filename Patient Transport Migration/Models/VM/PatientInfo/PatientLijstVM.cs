using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    public class PatientLijstVM {

        public PatientLijstVM() {
            var db = new MSSQLContext();
            //Query db naar patienten en sorteer
            _Patienten = db.tblPatienten.OrderBy(p => p.Achternaam).ToList();
        }

        public PatientLijstVM(string patient) : this() {
            SelectedPatient = patient;
        }

        [Display(Name = "Patient")]
        public string SelectedPatient { get; set; }

        private List<Patient> _Patienten { get; set; }
        public IEnumerable<SelectListItem> Patienten {
            get {
                return _Patienten.Select(t => new SelectListItem {
                    Value = t.PatientVisit.ToString(),
                    Text = t.Naam
                });
            }
        }
    }
}