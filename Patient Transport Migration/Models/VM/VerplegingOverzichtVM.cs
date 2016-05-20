using System.Collections.Generic;
using System.Linq;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM {
    public class VerplegingOverzichtVM {
        public VerplegingOverzichtVM() { }
        
        public static VerplegingOverzichtVM Create() {
            return new VerplegingOverzichtVM();
        }

        public VerplegingOverzichtPatientenInOrde PatientenInOrdeVM { get; set; }

        public VerplegingOverzichtPatientenWachtend PatientenWachtendVM { get; set; }
    }

    public class VerplegingOverzichtPatientenInOrde {
        public VerplegingOverzichtPatientenInOrde() { }

        public static VerplegingOverzichtPatientenInOrde Create() {
            var db = new MSSQLContext();
            var vm = new VerplegingOverzichtPatientenInOrde(); 

            // query de taken die eerst aan de wn gegeven zijn die nog niet uitgevoerd zijn die een patient bevatten
            var taakEntries = db.tblTransportTaken.Where(t => 
                !string.IsNullOrEmpty(t.Aanvraag.PatientId) && 
                t.DatumCompleet != null)
                .ToList();
            vm.PatientInOrde = taakEntries;

            return vm;
        }

        /// <summary>
        /// Lijst van <c>TransportTaak</c> met een <c>Patient</c> 
        /// die recentelijk verplaatst zijn door Transport personeel
        /// </summary>
        public List<TransportTaak> PatientInOrde { get; set; }
    }

    public class VerplegingOverzichtPatientenWachtend {
        public VerplegingOverzichtPatientenWachtend() { }

        public static VerplegingOverzichtPatientenWachtend Create() {
            var db = new MSSQLContext();
            var vm = new VerplegingOverzichtPatientenWachtend();

            // query de recente uitgevoerde taken met een patient
            var taakEntries = db.tblTransportTaken.Where(t =>
                !string.IsNullOrEmpty(t.Aanvraag.PatientId) &&
                t.DatumCompleet == null
                // TODO && t.DateComplete < 1 Dag
                ).ToList();
            vm.PatientenWachtend = taakEntries;

            return vm;
        }

        /// <summary>
        /// Lijst van <c>TransportTaak</c> met een <c>Patient</c>
        /// die verplaatst zullen worden door Transport personeel
        /// </summary>
        public List<TransportTaak> PatientenWachtend { get; set; }
    }
}