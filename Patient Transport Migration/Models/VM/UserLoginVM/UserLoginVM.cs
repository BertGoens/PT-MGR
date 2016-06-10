using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Patient_Transport_Migration.Models.VM.UserLoginVM {
    public class UserLoginVM {
        public UserLoginVM() {

        }

        [Required]
        public string Gebruikersnaam { get; set; }

        [Required]
        public string Paswoord { get; set; }
    }
}