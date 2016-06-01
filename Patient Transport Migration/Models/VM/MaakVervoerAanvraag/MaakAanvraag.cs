using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM.MaakVervoerAanvraag {
    /// <summary>
    /// Maak een nieuwe aanvraag op basis van een type
    /// </summary>
    public class MaakAanvraag {
        public MaakAanvraag() { }

        public MaakAanvraag(string aanvraagTypeId) {
            var db = new MSSQLContext();
            //maak aanvraag adhv gegeven type
            if (aanvraagTypeId.Length > 0) {
                // Laad type uit db
                try {
                    AanvraagTypeId = int.Parse(aanvraagTypeId);
                    _DisplayAanvraagTypeData = db.tblAanvraagTypes.First(a => a.Id == AanvraagTypeId);
                } catch (Exception ex) {
                    ex.ToString();
                    // InvalidOperationException => Sequence contains no elements
                    // FormatExeption => Slechte request
                    return;
                }

            }

            if (_DisplayAanvraagTypeData.Include_Patient || _DisplayAanvraagTypeData.Include_PatientVisit) {
                //Query db naar patienten & sorteer A-Z
                _patientenLijst = db.tblPatienten.OrderBy(patient => patient.Achternaam).ToList();
            }

            if (_DisplayAanvraagTypeData.Include_avr_Transportwijze) {
                _transportwijzeLijst = db.tblTransportwijzes.ToList();
            }
        }

        private AanvraagType _DisplayAanvraagTypeData;
        public AanvraagType DisplayAanvraagTypeData { get { return _DisplayAanvraagTypeData; } }

        /*
        private Aanvraag _DisplayAanvraagData;
        public Aanvraag DisplayAanvraagData { get { return _DisplayAanvraagData; } }
        */

        /// <summary>
        /// Null bij error
        /// </summary>
        public long? AanvraagId { get; set; }

        public int AanvraagTypeId { get; set; }

        [Display(Name = "Omschrijving")]
        public string va_Omschrijving { get; set; }

        [Display(Name = "Van")]
        public string Van { get; set; }

        [Display(Name = "Naar")]
        public string Naar { get; set; }

        private List<Patient> _patientenLijst;
        [Display(Name = "Selecteer de patiënt")]
        public string SelectedPatient { get; set; }
        public IEnumerable<SelectListItem> PatientenLijst {
            get {
                var result = _patientenLijst.Select(f => new SelectListItem {
                    Value = f.PatientVisit.ToString(),
                    Text = f.Naam
                });
                return result;
            }
        }

        private List<Transportwijze> _transportwijzeLijst;
        [Display(Name = "Selecteer de transportwijze")]
        public string SelectedTransportwijze { get; set; }
        public IEnumerable<SelectListItem> TransportwijzeLijst {
            get {
                var result = _transportwijzeLijst.Select(t => new SelectListItem {
                    Value = t.Id.ToString(),
                    Text = t.Omschrijving
                });
                return result;
            }
        }
    }
}