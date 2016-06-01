using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    /// <summary>
    /// Zoekt in de database naar AanvraagTypes die een patient bevatten
    /// </summary>
    public class AanvraagTypesVM {
        public AanvraagTypesVM() {
            var db = new MSSQLContext();
            _AanvraagTypes = db.tblAanvraagTypes.Where(a => a.Include_Patient || a.Include_PatientVisit).ToList();
        }

        //Wordt momenteel niet gebruikt voor POST parameters maar ik laat hem er bij staan indien we uitbreiden.
        public string AanvraagTypeSelected { get; set; }

        private List<AanvraagType> _AanvraagTypes { get; set; }
        public IEnumerable<SelectListItem> AanvraagTypeLijst {
            get {
                return _AanvraagTypes.Select(at => new SelectListItem {
                    Text = at.Omschrijving,
                    Value = at.Id.ToString()
                });
            }
        }
    }
}