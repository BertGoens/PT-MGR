using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Patient_Transport_Migration.Models.VM {
    public class MaakVervoerAanvraagVM {
        public MaakVervoerAanvraagVM() { }
        public static MaakVervoerAanvraagVM Create() {

        }

        public string AanvraagTypeGeselecteerd { get; set; }

        public SelectList AanvraagTypeLijst { get; set; }
    }
}