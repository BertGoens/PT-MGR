using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM {
    // Used by PatientInfo
    public class PatientInfoVM {
        public PatientInfoVM() { }

        public static PatientInfoVM Create() {
            var db = new MSSQLContext();
            var vm = new PatientInfoVM();

            //Query db naar patienten
            var patientEntries = db.tblPatienten.ToList();
            //Sort A-Z
            patientEntries.OrderBy(patient => patient.Achternaam);
            //Add to VM
            vm.PatientenLijst = new SelectList(patientEntries, "PatientVisit", "Naam"); //(Items / DataValueField / DataTextField)

            return vm;
        }

        public string PatientenLijstSelected { get; set; }

        /// <summary>
        /// Lijst van alle patienten
        /// </summary>
        public SelectList PatientenLijst { get; set; }

        public PatientDetailsVM PatientDetailsVM { get; set; }

        public PatientMedischeAanvragenVM PatientMedischeAanvragenVM { get; set; }

        public AanvraagTypesVM AanvraagTypesVM { get; set; }
    }

    // Used by _PatientDetails
    public class PatientDetailsVM {
        public PatientDetailsVM() { }

        public static PatientDetailsVM Create(string visitId = null) {
            var vm = new PatientDetailsVM();
            
            if (!string.IsNullOrEmpty(visitId)) {
                var db = new MSSQLContext();
                // Zoek de patient via visitId
                vm.PatientDetails = db.tblPatienten.Where(p => p.PatientVisit.Equals(visitId)).First();
            }

            return vm;
        }

        public Patient PatientDetails { get; set; }
    }

    public class PatientMedischeAanvragenVM {
        public PatientMedischeAanvragenVM() { }

        public static PatientMedischeAanvragenVM Create(string visitId = null) {
            var vm = new PatientMedischeAanvragenVM();      

            if (!string.IsNullOrEmpty(visitId)) {
                var db = new MSSQLContext();
                // Zoek alle medische aanvragen voor de patient
                vm.PatientAanvragen = db.tblAanvragen.Where(a => a.PatientVisit.Equals(visitId) && a.AanvraagType.Include_Patient == true).ToList();
            }

            return vm;
        }

        /// <summary>
        /// Lijst van medische <c>Aanvraag</c> van ... voor deze patient
        /// </summary>
        public List<Aanvraag> PatientAanvragen { get; set; }
    }

    // Used by _RequestTypes
    public class AanvraagTypesVM {
        public AanvraagTypesVM() { }

        public static AanvraagTypesVM Create() {
            var vm = new AanvraagTypesVM();
            var db = new MSSQLContext();

            //Query db voor [AanvraagType]s
            var aanvraagTypeEntries = db.tblAanvraagTypes.ToList();
            //Include in VM
            vm.AanvraagTypeLijst = new SelectList(aanvraagTypeEntries, "Id", "Omschrijving");

            return vm;
        }

        public string AanvraagTypeSelected { get; set; }

        public SelectList AanvraagTypeLijst { get; set; }
        
    }
       //public TransportTaak AanvraagDetails { get; set; }
}