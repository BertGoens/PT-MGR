using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    public class AanvraagDetailsVM {
        public AanvraagDetailsVM(string aanvraagId) {
            if (!string.IsNullOrEmpty(aanvraagId)) {
                try {
                    //Db opzoeken
                    var db = new Context();
                    long aId = long.Parse(aanvraagId.ToString());
                    Aanvraag = db.tblAanvragen.First(a => a.Id == aId);
                } catch (Exception ex) {
                    ex.ToString();
                }
            }

        }

        public Aanvraag Aanvraag { get; private set; }
    }
}