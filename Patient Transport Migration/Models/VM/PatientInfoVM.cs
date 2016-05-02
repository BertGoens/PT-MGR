using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM {
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

        public PatientCreateAanvraagVM PatientAanvraagDetailsVM { get; set; }
    }

    public class PatientDetailsVM {
        public PatientDetailsVM() { }

        public static PatientDetailsVM Create(string visitId = null) {
            var vm = new PatientDetailsVM();

            vm.PatientVisitId = visitId;

            if (!string.IsNullOrEmpty(visitId)) {
                var db = new MSSQLContext();
                // Zoek de patient via visitId
                vm.PatientDetails = db.tblPatienten.Where(p => p.PatientVisit.Equals(visitId)).First();
            }

            return vm;
        }

        public string PatientVisitId { get; set; }

        public Patient PatientDetails { get; set; }
    }

    public class PatientMedischeAanvragenVM {
        public PatientMedischeAanvragenVM() { }

        public static PatientMedischeAanvragenVM Create(string visitId = null) {
            var vm = new PatientMedischeAanvragenVM();

            vm.PatientVisitId = visitId;

            if (!string.IsNullOrEmpty(visitId)) {
                var db = new MSSQLContext();
                // Zoek alle medische aanvragen voor de patient
                vm.PatientAanvragen = db.tblAanvragen.Where(a => a.PatientVisit.Equals(visitId) && a.AanvraagType.Include_Patient == true).ToList();
            }

            return vm;
        }

        public string PatientVisitId { get; set; }

        /// <summary>
        /// Lijst van medische <c>Aanvraag</c> van ... voor deze patient
        /// </summary>
        public List<Aanvraag> PatientAanvragen { get; set; }
    }

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

    public class PatientCreateAanvraagVM {
        public PatientCreateAanvraagVM() { }

        public static PatientCreateAanvraagVM Create(string aanvraagTypeId = null) {
            var vm = new PatientCreateAanvraagVM();

            if (!string.IsNullOrEmpty(aanvraagTypeId)) {
                int aanvrType;
                bool aanvraagTypeIsInt = int.TryParse(aanvraagTypeId, out aanvrType);
                if (aanvraagTypeIsInt) {
                    var db = new MSSQLContext();
                    var aanvraagEntry = db.tblAanvraagTypes.First(at => at.Id == aanvrType);
                    vm.PatientCreateAanvraagType = aanvraagEntry;
                }
            }

            return vm;
        }

        /// <summary>
        /// Type aanvraag om te maken
        /// </summary>
        public AanvraagType PatientCreateAanvraagType { get; set; }

        /// <summary>
        /// Bedoeld voor POST naar de server
        /// </summary>
        public Aanvraag PatientCreateAanvraag { get; set; }
    }
}