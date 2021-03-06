﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.VM.MaakVervoerAanvraag {
    public class AanvraagTypesVM {
        public AanvraagTypesVM(string patient, string type) {
            var db = new Context();
            _AanvraagTypes = db.tblAanvraagTypes.ToList();

            //State onthouden
            SelectedPatient = patient;
            SelectedAanvraagTypeId = type;
        }
     
        public string SelectedPatient { get; private set; }

        public string SelectedAanvraagTypeId { get; set; }
        private List<AanvraagType> _AanvraagTypes { get; set; }
        public IEnumerable<SelectListItem> AanvraagTypeLijst {
            get {
                return _AanvraagTypes.Select(a => new SelectListItem {
                    Text = a.Omschrijving,
                    Value = a.Id.ToString()
                });
            }
        }
    }
}