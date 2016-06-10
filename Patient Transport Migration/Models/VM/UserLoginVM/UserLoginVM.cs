using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patient_Transport_Migration.Models.VM.UserLoginVM {
    public class UserLoginVM {
        public UserLoginVM() {

        }

        public string Gebruikersnaam { get; set; }
        public string Paswoord { get; set; }
    }
}