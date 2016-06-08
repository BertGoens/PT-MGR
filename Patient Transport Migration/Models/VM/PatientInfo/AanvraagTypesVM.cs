using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration.Models.VM.PatientInfo {
    /// <summary>
    /// Zoekt in de database naar AanvraagTypes die een patient bevatten
    /// </summary>
    public class AanvraagTypesVM {
        public AanvraagTypesVM(string patientId, Context context) {
            var db = new Context();
            _AanvraagTypes = new AanvraagTypeRepository(context).GetAanvraagTypesWithPatient();
            PatientId = patientId;
        }

        private IEnumerable<AanvraagType> _AanvraagTypes { get; set; }
        public IEnumerable<SelectListItem> AanvraagTypeLijst {
            get {
                return _AanvraagTypes.Select(at => new SelectListItem {
                    Text = at.Omschrijving,
                    Value = at.Id.ToString()
                });
            }
        }

        public string PatientId { get; private set; }
    }
}