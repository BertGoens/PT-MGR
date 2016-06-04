using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patient_Transport_Migration.Models.VM.VerplegingOverzicht {
    public class VerplegingDienstenLijstVM {
        public VerplegingDienstenLijstVM(string Dienst, string PatientenOkPage, string PatientenWachtendPage) {
            //TODO List Diensten
            this.PatientenOkPage = PatientenOkPage;
            this.PatientenWachtendPage = PatientenWachtendPage;
        }

        // Onthoud State van de pagina
        public string PatientenOkPage { get; private set; }
        public string PatientenWachtendPage { get; private set; }

        private List<string> diensten { get; set; }
    }
}