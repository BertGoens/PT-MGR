using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Model;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration.Models.VM.MaakVervoerAanvraag {

    public class MaakAanvraag {
        public MaakAanvraag() {
            // Required for POST
        }
        public MaakAanvraag(string aanvraagTypeId, string patient, Context context) {
            var db = new Context();
            //maak aanvraag adhv gegeven type
            if (aanvraagTypeId.Length > 0) {
                // Laad type uit db
                try {
                    AanvraagTypeId = int.Parse(aanvraagTypeId);
                    _DisplayAanvraagTypeData = db.tblAanvraagTypes.First(a => a.Id == AanvraagTypeId);

                    if (_DisplayAanvraagTypeData.Include_Patient) {
                        //Query db naar patienten & sorteer A-Z
                        if(!string.IsNullOrEmpty(patient)) {
                            _patientenLijst = new List<Patient> { new PatientRepository(context).GetPatientByVisitId(patient) };
                        } else {
                            _patientenLijst = new PatientRepository(context).GetPatientenOrderByAchternaam();
                        }                     
                    }

                    if (_DisplayAanvraagTypeData.Include_Transportwijze) {
                        _transportwijzeLijst = db.tblTransportwijzes.ToList();
                    }

                    if (_DisplayAanvraagTypeData.Include_AanDokter) {
                        _dokterLijst = new Context().tblDokters.ToList();
                    }

                    _afdelingLijst = new LocatieRepository(context).GetUniekeAfdelingen();
                } catch (Exception ex) {
                    ex.ToString();
                    // InvalidOperationException => Sequence contains no elements
                    // FormatExeption => Slechte request
                    return;
                }
            }
        }

        private AanvraagType _DisplayAanvraagTypeData;
        public AanvraagType DisplayAanvraagTypeData { get { return _DisplayAanvraagTypeData; } }

        public long? AanvraagId { get; set; }

        public int AanvraagTypeId { get; set; }

        [Display(Name = "Aanvraag voor")]
        public DateTime DatumAanvraag { get; set; }

        [Display(Name = "Omschrijving")]
        [DataType(DataType.MultilineText)]
        public string Omschrijving { get; set; }

        [Display(Name = "CT Scan")]
        public bool CT { get; set; }
        public bool NMR { get; set; }
        public bool RX { get; set; }
        public bool Echografie { get; set; }

        private IEnumerable<Afdeling> _afdelingLijst;
        [Display(Name = "Afdeling")]
        public string AfdelingSelected { get; set; }
        public IEnumerable<SelectListItem> AfdelingLijst {
            get {
                var result = _afdelingLijst.Select(l =>
                new SelectListItem {
                    Text = l.Omschrijving,
                    Value = l.Code,
                });
                return result;
            }
        }
        [Display(Name = "Kamer")]
        public string KamerSelected { get; set; }

        private List<Dokter> _dokterLijst;
        [Display(Name = "Selecteer de dokter")]
        public string DokterSelected { get; set; }
        public IEnumerable<SelectListItem> DokterLijst { get {
                var result = _dokterLijst.Select(d => new SelectListItem {
                    Value = d.Id,
                    Text = d.Naam
                });
                return result;
            } }

        private IEnumerable<Patient> _patientenLijst;
        [Display(Name = "Selecteer de patiënt")]
        public string PatientSelected { get; set; }
        public IEnumerable<SelectListItem> PatientenLijst {
            get {
                IEnumerable<SelectListItem> result = null;
                try {
                    result = _patientenLijst.Select(f => new SelectListItem {
                        Value = f.PatientVisit.ToString(),
                        Text = f.Naam
                    });
                } catch (Exception) {
                }
                return result;
            }
        }

        private List<Transportwijze> _transportwijzeLijst;
        [Display(Name = "Selecteer de transportwijze")]
        public string TransportwijzeSelected { get; set; }
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