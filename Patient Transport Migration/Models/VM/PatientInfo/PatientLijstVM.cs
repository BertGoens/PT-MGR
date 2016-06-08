using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    public class PatientLijstVM {

        public PatientLijstVM(Context context) {
            _Patienten = new PatientRepository(context).GetPatientenOrderByAchternaam();
        }

        public PatientLijstVM(Context context, string patient) : this(context) {
            SelectedPatient = patient;
        }

        [Display(Name = "Patient")]
        public string SelectedPatient { get; set; }

        private IEnumerable<Patient> _Patienten { get; set; }
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