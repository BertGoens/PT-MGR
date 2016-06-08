using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.Core;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Repositories;
using Patient_Transport_Migration.Models.VM;

namespace Patient_Transport_Migration.Controllers {

    public class HomeController : Controller {

        Context _context = new Context();

        private string getViewModelErrors() {
            return string.Join(",", ModelState.Values.Where(e => e.Errors.Count > 0)
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray());
        }

        [HttpGet]
        public ViewResult Index() {
            return View();
        }

        #region DokterStatus
        [HttpGet]
        public ViewResult DokterStatus() {
            var DoktersExclusiefRadiologie = new DokterRepository(_context).GetDoktersExcludeRadiologie();
            return View("./DokterStatus/DokterStatus", new Models.VM.DokterStatus.DokterLijstVM(DoktersExclusiefRadiologie));
        }

        [HttpGet]
        public PartialViewResult GetDokterStatus_DokterDetails(string docterId) {
            var DokterDetails = _context.tblDokters.Single(d => d.Id == docterId);
            var vm = new Models.VM.DokterStatus.DokterDetailsVM(DokterDetails);
            return PartialView("./DokterStatus/_EditDokter", vm);
        }

        [HttpPost]
        public ActionResult DokterStatus(Models.VM.DokterStatus.DokterDetailsVM vm) {
            if (ModelState.IsValid) {
                // Haal dokter op & bewerk zijn instellingen
                try {
                    Dokter dr = _context.tblDokters.Find(vm.Id);
                    if (dr.IsConsultVerwachtend != vm.IsConsultVerwachtend) {
                        dr.IsConsultVerwachtend = vm.IsConsultVerwachtend;
                        //Save doctor consult status
                        _context.Entry(dr).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                } catch (Exception ex) {
                    //TODO LOG
                    ViewBag.ErrorMessage = ex.Message;
                    return DokterStatus();
                }
                return RedirectToAction("DokterStatus");
            } else {
                var errors = getViewModelErrors();
                ViewBag.ErrorMessage = errors;
            }

            return DokterStatus();
        }
        #endregion DokterStatus

        #region PatientInfo
        [HttpGet]
        public ViewResult PatientInfo(string visitId) {
            return View("./PatientInfo/PatientInfo", new Models.VM.PatientInfo.PatientLijstVM(_context,visitId));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_PatientDetails(string visitId) {
            return PartialView("./PatientInfo/_PatientDetails", new Models.VM.PatientInfo.PatientDetailsVM(visitId, _context));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_MedischeAanvragen(string visitId, string page) {
            return PartialView("./PatientInfo/_PatientMedischeAanvragen", new Models.VM.PatientInfo.PatientTransportAanvragenVM(_context, visitId, page));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_AanvraagTypes(string visitId) {
            return PartialView("./PatientInfo/_AanvraagTypesPatient", new Models.VM.PatientInfo.AanvraagTypesVM(visitId, _context));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_AanvraagDetails(string aanvraagId) {
            var vm = new Models.VM.PatientInfo.AanvraagDetailsVM(aanvraagId);
            return PartialView("./PatientInfo/_AanvraagDetailsPatient", vm);
        }

        #endregion PatientInfo

        #region VerplegingOverzicht
        [HttpGet]
        public ViewResult VerplegingOverzicht(string afdeling) {
            return View("./VerplegingOverzicht/VerplegingOverzicht",
                new Models.VM.VerplegingOverzicht.VerplegingDienstenLijstVM(_context, afdeling));
        }

        [HttpGet]
        public PartialViewResult GetPatientenInOrde(string page, string Afdeling) {
            return PartialView("./VerplegingOverzicht/_PatientenInOrde", new Models.VM.VerplegingOverzicht.RecentVervoerdePatienten(_context, page, Afdeling));
        }

        [HttpGet]
        public PartialViewResult GetPatientenWachtend(string page, string Afdeling) {
            return PartialView("./VerplegingOverzicht/_PatientenWachtend", new Models.VM.VerplegingOverzicht.PatientenTeVerplaatsenVM(_context, page, Afdeling));
        }
        #endregion VerplegingOverzicht

        #region MaakVervoerAanvraag
        [HttpGet]
        public ViewResult MaakVervoerAanvraag(string patient, string type) {
            return View("./MaakVervoerAanvraag/MaakVervoerAanvraag", new Models.VM.MaakVervoerAanvraag.AanvraagTypesVM(patient, type));
        }

        [HttpGet]
        public PartialViewResult GetMaakVervoerAanvraag_AanvraagDetails(string aanvraagTypeId, string patient) {
            return PartialView("./MaakVervoerAanvraag/_AanvraagDetails", new Models.VM.MaakVervoerAanvraag.MaakAanvraag(aanvraagTypeId, patient, _context));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LaadKamersVanAfdeling(string AfdelingCode) {
            var locatieLijst = new LocatieRepository(_context).GetKamersVanAfdeling(AfdelingCode);

            if (locatieLijst != null) {
                var AfdelingKamers = locatieLijst.Select(l => new SelectListItem() {
                    Text = l.Kamer,
                    Value = l.Kamer
                });
                return Json(AfdelingKamers, JsonRequestBehavior.AllowGet);
            }

            return Json(new SelectList(Enumerable.Empty<SelectListItem>()), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MaakVervoerAanvraag(Models.VM.MaakVervoerAanvraag.MaakAanvraag vm) {
            if (ModelState.IsValid) {                
                //Maak Aanvraag
                var aanvraagData = new Aanvraag();

                aanvraagData.DatumAanvraag = DateTime.Now;
                aanvraagData.AanvraagDoor = User.Identity.Name;                
                aanvraagData.Omschrijving = vm.Omschrijving;

                aanvraagData.AanvraagType = _context.tblAanvraagTypes.First(at => at.Id == vm.AanvraagTypeId);

                if (aanvraagData.AanvraagType.Include_Patient) {
                    aanvraagData.Patient = _context.tblPatienten.First(p => p.PatientVisit == vm.PatientSelected);                    

                    if (aanvraagData.AanvraagType.Include_Transportwijze) {
                        int transportwijze = int.Parse(vm.TransportwijzeSelected);
                        aanvraagData.Transportwijze = _context.tblTransportwijzes.First(p => p.Id == transportwijze);
                    }

                    if (aanvraagData.AanvraagType.Include_AanDokter) {
                        aanvraagData.AanDokter = _context.tblDokters.First(d => d.Id == vm.DokterSelected);
                    }

                    if (aanvraagData.AanvraagType.Include_Radiologie) {
                        aanvraagData.CT = vm.CT;
                        aanvraagData.NMR = vm.NMR;
                        aanvraagData.RX = vm.RX;
                        aanvraagData.Echografie = vm.Echografie;
                    }
                }

                _context.tblAanvragen.Add(aanvraagData);
                // SAVE
                _context.SaveChanges();

                //Verwerk Aanvraag    
                var locKamer = _context.tblLocaties.First(l => l.Kamer == vm.KamerSelected);
                BehandelAanvraag.NieuweAanvraag(_context, aanvraagData, locKamer);               
            } else {
                var errors = getViewModelErrors();
                ViewBag.ErrorMessage = errors;
                return View("./MaakVervoerAanvraag/MaakVervoerAanvraag");
            }
            return RedirectToAction("MaakVervoerAanvraag");
        }
        #endregion MaakVervoerAanvraag

        #region DispatchOverzicht
        [HttpGet]
        public ViewResult DispatchOverzicht(string TakenPage) {
            return View("./DispatchOverzicht/DispatchOverzicht", new Models.VM.DispatchOverzicht.WerknemersVM(TakenPage));
        }

        [HttpGet]
        public PartialViewResult GetDispatchOverzicht_WachtendeTransportTaken(string page) {
            int pageNr;
            int.TryParse(page, out pageNr);
            return PartialView("./DispatchOverzicht/_WachtendeTransportTaken", new Models.VM.DispatchOverzicht.WachtendeTransportTakenVM(pageNr));
        }

        [HttpGet]
        public JsonResult DispatchOverzicht_GetWerknemerTaken(string WerknemerId) {
            var Taken = new TransportTaakRepository(_context).GetWerknemerTakenQueue(WerknemerId).ToList();
            return Json(Taken, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DispatchOverzicht_SaveWerknemerSchema(string jsonSchema) {
            if (!string.IsNullOrEmpty(jsonSchema)) {
                try {
                    dynamic data = System.Web.Helpers.Json.Decode(jsonSchema);
                    string WerknemerId = data.WerknemerId;
                    System.Web.Helpers.DynamicJsonArray Taken = data.Taken;

                    int wachtrijNummer = 0;
                    foreach (var taakId in Taken) {
                        int tId = int.Parse(taakId.ToString());
                        var taak = _context.tblTransportTaken.First(t =>
                        t.Id == tId);
                        if (taak.TaakWachtrijNummer != wachtrijNummer) {
                            taak.TaakWachtrijNummer = wachtrijNummer;
                            _context.Entry(taak).State = EntityState.Modified;
                        }

                        wachtrijNummer++;
                    }
                    _context.SaveChanges();
                    return true;
                } catch (Exception ex) {
                    _context.tblExceptionLogger.Add(new ExceptionLogger() {
                        ExceptionMessage = ex.Message,
                        ExceptionStackTrace = ex.StackTrace
                    });
                }
            }
            return false;
        }

        [HttpPost]
        public bool DispatchOverzicht_RemoveWerknemerFromTaak(string jsonTaak) {
            if (!string.IsNullOrEmpty(jsonTaak)) {
                try {
                    dynamic data = System.Web.Helpers.Json.Decode(jsonTaak);
                    int taakId = int.Parse(data.TaakId);
                    string werknemerId = data.WerknemerId;

                    var TaakRepo = new TransportTaakRepository(_context);

                    // Zoek de taak
                    var taak = TaakRepo.GetTransportTaakByWerknemerByNumber(werknemerId, taakId);

                    int taakNummerInQueue = (int)taak.TaakWachtrijNummer;
                    // Pas de taak in kwestie aan 
                    taak.TransportWerknemer = null;
                    taak.TransportWerknemerId = null;
                    _context.Entry(taak).State = EntityState.Modified;

                    // Pas alle taken erna aan (-1)
                    var takenErnaa = TaakRepo.GetTakenInQueueForMedewerkenNa(werknemerId, taakNummerInQueue);
                    
                    foreach (var t in takenErnaa) {
                        t.TaakWachtrijNummer -= 1;
                        _context.Entry(t).State = EntityState.Modified;
                    }
                    _context.SaveChanges();
                    return true;
                } catch (Exception ex) {
                    _context.tblExceptionLogger.Add(new ExceptionLogger() {
                        ExceptionMessage = ex.Message,
                        ExceptionStackTrace = ex.StackTrace
                    });
                }
            }
            return false;
        }

        [HttpPost]
        public bool DispatchOverzicht_DeleteTaak(string jsonTaak) {
            if (!string.IsNullOrEmpty(jsonTaak)) {
                try {
                    dynamic data = System.Web.Helpers.Json.Decode(jsonTaak);
                    long taakId = data.TaakId;
                    var taak = _context.tblTransportTaken.First(t => t.Id == taakId);
                    taak.DatumCompleet = DateTime.Now;
                    _context.Entry(taak).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                } catch (Exception ex) {
                    _context.tblExceptionLogger.Add(new ExceptionLogger() {
                        ExceptionMessage = ex.Message,
                        ExceptionStackTrace = ex.StackTrace
                    });
                }
            }
            return false;
        }

        [HttpPost]
        public bool DispatchOverzicht_SaveTaak(string jsonTaak) {
            if (!string.IsNullOrEmpty(jsonTaak)) {
                try {
                    dynamic data = System.Web.Helpers.Json.Decode(jsonTaak);
                    long taakId = data.TaakId;
                    string TaakNotities = data.TaakNotities;
                    bool isHogePrioriteit = data.TaakIsHogePrioriteit;
                    string TaakWerknemerGebruikersnaam = data.TaakWerknemerId;

                    var WernemerRepo = new TransportWerknemerRepository(_context);

                    var Taak = _context.tblTransportTaken.First(t => t.Id == taakId);
                    Taak.IsPrioriteitHoog = isHogePrioriteit;
                    Taak.TransportNotities = TaakNotities;
                    var TWerknemer = WernemerRepo.GetWerknemerByGebruikersnaam(TaakWerknemerGebruikersnaam);
                    Taak.TransportWerknemer = TWerknemer;

                    Taak.TaakWachtrijNummer = new TransportTaakRepository(_context)
                        .GetWerknemerTakenQueue(TaakWerknemerGebruikersnaam)
                        .Count();

                    _context.Entry(Taak).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                } catch (Exception ex) {
                    _context.tblExceptionLogger.Add(new ExceptionLogger() {
                        ExceptionMessage = ex.Message,
                        ExceptionStackTrace = ex.StackTrace
                    });
                }            
            }
            return false;
        }
        #endregion DispatchOverzicht

        #region TransportMedewerker
        [HttpGet]
        public PartialViewResult TransportMedewerker() {
            //TODO naar User.Identity overschakelen
            string id = "sta_it2";
            return PartialView("./TransportMedewerker/TransportMedewerker", new Models.VM.TransportMedewerker.TransportTakenVM(_context, id));
        }

        [HttpPost]
        public bool TaakStart(string taak) {
            return true;
        }

        [HttpPost]
        public bool TaakVolbracht(string taak) {
            return true;
        }
        #endregion
    }
}