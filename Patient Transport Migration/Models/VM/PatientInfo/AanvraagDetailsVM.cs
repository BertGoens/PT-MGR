using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    public class AanvraagDetailsVM {
        public AanvraagDetailsVM() {

        }

        public AanvraagDetailsVM(string aanvraagId) {
            if (!string.IsNullOrEmpty(aanvraagId)) {
                try {
                    //Db opzoeken
                    var db = new MSSQLContext();
                    long aId = long.Parse(aanvraagId.ToString());
                    Aanvraag = db.tblAanvragen.First(a => a.Id == aId);
                    _transportwijzeValues = db.tblTransportwijzes.ToList();
                    
                } catch (Exception ex) {
                    ex.ToString();
                }
            }

        }

        public Aanvraag Aanvraag { get; private set; }
        public string AanvraagId { get; set; }
        public Patient Patient { get; private set; }
        public string PatientVisitId { get; set; }

        [Display(Name = "Transportwijze")]
        public string SelectedTransportwijze { get; set; }
        private List<Transportwijze> _transportwijzeValues;
        public IEnumerable<SelectListItem> TransportwijzeLijst {
            get {
                return _transportwijzeValues.Select(t => new SelectListItem {
                    Value = t.Id.ToString(),
                    Text = t.Omschrijving
                });
            }
        }

        [Display(Name = "Omschrijving van de taak")]
        public string va_Omschrijving { get; set; }
    }
}