using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.VM;

namespace Patient_Transport_Migration.Controllers {

    public class HomeController : Controller {

        private MSSQLContext db = new MSSQLContext();

        private string getViewModelErrors() {
            return string.Join(",", ModelState.Values.Where(e => e.Errors.Count > 0)
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray());
        }

        [HttpGet]
        public ActionResult Index() {
            return View();
        }

        #region DokterStatus
        [HttpGet]
        public ViewResult DokterStatus() {
            return View("./DokterStatus/DokterStatus", new Models.VM.DokterStatus.DokterLijstVM());
        }

        [HttpGet]
        public PartialViewResult GetDokterStatus_DokterDetails(string docterId) {
            var vm = new Models.VM.DokterStatus.DokterDetailsVM(docterId);
            return PartialView("./DokterStatus/_EditDokter", vm);
        }

        [HttpPost]
        public ActionResult DokterStatus(Models.VM.DokterStatus.DokterDetailsVM vm) {
            if (ModelState.IsValid) {
                // Haal dokter op & bewerk zijn instellingen
                try {
                    Dokter dr = db.tblDokters.First(d => d.Id == vm.Id);
                    if (dr.IsConsultVerwachtend != vm.IsConsultVerwachtend) {
                        dr.IsConsultVerwachtend = vm.IsConsultVerwachtend;
                        //Save doctor consult status
                        db.Entry(dr).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                } catch (Exception ex) {
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
            return View("./PatientInfo/PatientInfo", new Models.VM.PatientInfo.PatientLijstVM(visitId));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_PatientDetails(string visitId) {
            return PartialView("./PatientInfo/_PatientDetails", new Models.VM.PatientInfo.PatientDetailsVM(visitId));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_MedischeAanvragen(string visitId, string page) {
            return PartialView("./PatientInfo/_PatientMedischeAanvragen", new Models.VM.PatientInfo.PatientTransportAanvragenVM(visitId, page));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_AanvraagTypes(string visitId) {
            return PartialView("./PatientInfo/_AanvraagTypes", new Models.VM.PatientInfo.AanvraagTypesVM(visitId));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_AanvraagDetails(string aanvraagId) {
            var vm = new Models.VM.PatientInfo.AanvraagDetailsVM(aanvraagId);
            return PartialView("./PatientInfo/_AanvraagDetails", vm);
        }

        [HttpPost]
        public ActionResult PatientInfo(Models.VM.PatientInfo.AanvraagDetailsVM vm) {
            if (ModelState.IsValid) {
                try {
                    long anvId = long.Parse(vm.AanvraagId);
                    Aanvraag aanvraag = db.tblAanvragen.First(a => a.Id == anvId);

                    if (aanvraag.AanvraagType.Include_avr_Transportwijze) {
                        int twId = int.Parse(vm.SelectedTransportwijze);
                        Transportwijze aanvrTw = db.tblTransportwijzes.First(tw => tw.Id == twId);
                        aanvraag.avr_Transportwijze = aanvrTw;
                    }

                    if (aanvraag.AanvraagType.Include_va_Omschrijving) {
                        aanvraag.va_Omschrijving = vm.va_Omschrijving;
                    }

                    db.Entry(aanvraag).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("PatientInfo", new Models.VM.PatientInfo.PatientLijstVM(vm.PatientVisitId));
                } catch (Exception ex) {
                    Debug.Print(ex.Message);
                    ViewBag.ErrorMessage = ex.Message;
                    return View("./PatientInfo/PatientInfo", new Models.VM.PatientInfo.PatientLijstVM(vm.PatientVisitId));
                }
            } else {
                ViewBag.ErrorMessage = getViewModelErrors();
                return View("./PatientInfo/PatientInfo", new Models.VM.PatientInfo.PatientLijstVM(vm.PatientVisitId));
            }
        }

        #endregion PatientInfo

        #region VerplegingOverzicht
        [HttpGet]
        public ViewResult VerplegingOverzicht(string dienst, string PatientenOkPage, string PatientenWachtendPage) {
            return View("./VerplegingOverzicht/VerplegingOverzicht",
                new Models.VM.VerplegingOverzicht.VerplegingDienstenLijstVM(dienst, PatientenOkPage, PatientenWachtendPage));
        }

        [HttpGet]
        public PartialViewResult GetPatientenInOrde(string page) {
            return PartialView("./VerplegingOverzicht/_PatientenInOrde", new Models.VM.VerplegingOverzicht.RecentVervoerdePatienten(page));
        }

        [HttpGet]
        public PartialViewResult GetPatientenWachtend(string page) {
            return PartialView("./VerplegingOverzicht/_PatientenWachtend", new Models.VM.VerplegingOverzicht.PatientenTeVerplaatsenVM(page));
        }
        #endregion VerplegingOverzicht

        #region MaakVervoerAanvraag
        [HttpGet]
        public ViewResult MaakVervoerAanvraag(string patient, string type) {
            return View("./MaakVervoerAanvraag/MaakVervoerAanvraag", new Models.VM.MaakVervoerAanvraag.AanvraagTypesVM(patient, type));
        }

        [HttpGet]
        public PartialViewResult GetMaakVervoerAanvraag_AanvraagDetails(string aanvraagTypeId, string patient) {
            return PartialView("./MaakVervoerAanvraag/_AanvraagDetails", new Models.VM.MaakVervoerAanvraag.MaakAanvraag(aanvraagTypeId, patient));
        }

        [HttpPost]
        public ActionResult MaakVervoerAanvraag(Models.VM.MaakVervoerAanvraag.MaakAanvraag vm) {
            if (ModelState.IsValid) {
                //Maak Aanvraag
                var saveAanvraag = new Aanvraag();
                saveAanvraag.AanvraagDoor = User.Identity.Name;
                saveAanvraag.AanvraagType = db.tblAanvraagTypes.First(at => at.Id == vm.AanvraagTypeId);

                if (saveAanvraag.AanvraagType.Include_va_Omschrijving) {
                    saveAanvraag.va_Omschrijving = vm.va_Omschrijving;
                }

                if (saveAanvraag.AanvraagType.Include_Patient) {
                    saveAanvraag.Patient = db.tblPatienten.First(p => p.PatientVisit == vm.SelectedPatient);
                }

                db.tblAanvragen.Add(saveAanvraag);

                //Maak TransportTaak
                var aanvraagTaak = new TransportTaak();
                aanvraagTaak.Aanvraag = saveAanvraag;
                aanvraagTaak.LocatieStart = vm.Van;
                aanvraagTaak.LocatieEind = vm.Naar;
                //aanvraagTaak.DatumGemaakt = new System.DateTime();
                db.tblTransportTaken.Add(aanvraagTaak);
                // SAVE
                db.SaveChanges();
            } else {
                Debug.Print(getViewModelErrors());
                return View("./MaakVervoerAanvraag/MaakVervoerAanvraag");
            }
            return RedirectToAction("MaakVervoerAanvraag");
        }
        #endregion MaakVervoerAanvraag

        #region DispatchOverzicht
        [HttpGet]
        public ActionResult DispatchOverzicht(string TakenPage) {
            return View("./DispatchOverzicht/DispatchOverzicht", new Models.VM.DispatchOverzicht.WerknemersVM(TakenPage));
        }

        [HttpGet]
        public PartialViewResult GetDispatchOverzicht_WachtendeTransportTaken(string page) {
            int pageNr;
            int.TryParse(page, out pageNr);
            return PartialView("./DispatchOverzicht/_WachtendeTransportTaken", new Models.VM.DispatchOverzicht.WachtendeTransportTakenVM(pageNr));
        }

       [HttpGet]
       public string DispatchOverzicht_GetWerknemerTaken(string WerknemerId) {
            var Taken = db.tblTransportTaken.Where(t =>
            t.TransportWerknemerId == WerknemerId &&
            t.DatumCompleet == null)
            .ToList();
            var jsonTaken = System.Web.Helpers.Json.Encode(Taken);
            return jsonTaken;
        }

        [HttpPost]
        public bool DispatchOverzicht_DeleteTaak(string jsonTaak) {
            if (!string.IsNullOrEmpty(jsonTaak)) {
                try {
                    dynamic data = System.Web.Helpers.Json.Decode(jsonTaak);
                    long taakId = data.TaakId;
                    var taak = db.tblTransportTaken.First(t => t.Id == taakId);
                    taak.DatumCompleet = DateTime.Now;
                    return true;
                } catch (Exception ex) {
                    Debug.Print(ex.Message);
                    throw;
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
                    string TaakWerknemerId = data.TaakWerknemerId;

                    var Taak = db.tblTransportTaken.First(t => t.Id == taakId);
                    Taak.IsPrioriteitHoog = isHogePrioriteit;
                    Taak.TransportNotities = TaakNotities;
                    var TWerknemer = db.tblTransportWerknemers.First(w => w.Gebruikersnaam == TaakWerknemerId);
                    Taak.TransportWerknemer = TWerknemer;

                    int TakenVoorWerknemer = db.tblTransportTaken.Where(t => 
                    t.TransportWerknemerId == TWerknemer.Gebruikersnaam
                    && t.DatumCompleet == null
                    ).Count();
                    Taak.TaakWachtrijNummer = TakenVoorWerknemer;

                    db.Entry(Taak).State = EntityState.Modified;
                    db.SaveChanges();
                } catch (Exception ex) {
                    Debug.Print(ex.Message);
                    throw;
                }

                return true;
            }
            return false;
        }
        #endregion DispatchOverzicht
    }
}