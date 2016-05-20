using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM {
    public class AanvraagVM {
        public AanvraagVM() { }

        public static AanvraagVM Create(string aanvraagId, string aanvraagTypeId, string userName) {
            var db = new MSSQLContext();
            var vm = new AanvraagVM();
            //probeer aanvraag te laden uit db
            if (aanvraagId.Length > 0) {
                var lngAanvraagId = long.Parse(aanvraagId);
                try {
                    vm.Aanvraag = db.tblAanvragen.First(a => a.Id == lngAanvraagId);
                } catch (InvalidOperationException) {
                    // Sequence contains no elements
                    throw;
                }
                
            } else {
                //maak aanvraag adhv gegeven type
                if (aanvraagTypeId.Length > 0) {
                    vm.Aanvraag = new Aanvraag();
                    // Laad type uit db
                    var intAanvraagType = int.Parse(aanvraagTypeId);
                    vm.Aanvraag.AanvraagType = db.tblAanvraagTypes.First(a => a.Id == intAanvraagType);
                    vm.Aanvraag.AanvraagDoor = userName;
                }
            }
            return vm;
        }

        public string AanvraagId { get; set; }
        public Aanvraag Aanvraag { get; set; }

        public string AanvraagTypeId { get; set; }
    }
}