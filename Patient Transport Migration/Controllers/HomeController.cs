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
            var vm = new Models.VM.DokterStatus.DokterLijstVM();
            return View(vm);
        }

        [HttpGet]
        public PartialViewResult GetDokterStatus_DokterDetails(string docterId) {
            var vm = new Models.VM.DokterStatus.DokterDetailsVM(docterId);
            return PartialView("_DokterStatus_EditDokter", vm);
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
                }
                return RedirectToAction("DokterStatus");
            } else 
           {
                var errors = getViewModelErrors();
                ViewBag.ErrorMessage = errors;
            }

            return View();
        }
        #endregion DokterStatus

        #region PatientInfo
        [HttpGet]
        public ViewResult PatientInfo(string patient) {
            return View(new Models.VM.PatientInfo.PatientLijstVM(patient));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_PatientDetails(string visitId) {
            return PartialView("_PatientInfo_PatientDetails", new Models.VM.PatientInfo.PatientDetailsVM(visitId));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_MedischeAanvragen(string visitId) {
            return PartialView("_PatientInfo_PatientMedischeAanvragen", new Models.VM.PatientInfo.PatientTransportAanvragenVM(visitId));
        }

        [HttpGet]
        public PartialViewResult GetPatientInfo_AanvraagTypes() {
            return PartialView("_PatientInfo_AanvraagTypes", new Models.VM.PatientInfo.AanvraagTypesVM());
        }

        /// <summary>
        /// Het updaten van een aanvraag
        /// </summary>
        [HttpGet]
        public PartialViewResult GetPatientInfo_AanvraagDetails(string aanvraagId) {
            var vm = new Models.VM.PatientInfo.AanvraagDetailsVM(aanvraagId);
            return PartialView("_PatientInfo_AanvraagDetails", vm);
        }

        /// <summary>
        /// POST van _PatientInfo_MedischeAanvragen
        /// </summary>
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
                    return View("PatientInfo", new Models.VM.PatientInfo.PatientLijstVM(vm.PatientVisitId));
                } catch (Exception ex) {
                    Debug.Print(ex.Message);
                    ViewBag.ErrorMessage = ex.Message;
                    throw;
                }
            } else {
                ViewBag.ErrorMessage = getViewModelErrors();
            }
            return RedirectToAction("PatientInfo", new Models.VM.PatientInfo.PatientLijstVM(vm.PatientVisitId));
        }

        #endregion PatientInfo

        #region VerplegingOverzicht
        [HttpGet]
        public ViewResult VerplegingOverzicht(string dienst) {
            return View(new Models.VM.VerplegingOverzicht.VerplegingDienstenLijstVM(dienst));
        }

        [HttpGet]
        public PartialViewResult GetPatientenInOrde() {
            return PartialView("_VerplegingOverzichtPatientenInOrde", new Models.VM.VerplegingOverzicht.PatientenInOrdeVM());
        }

        [HttpGet]
        public PartialViewResult GetPatientenWachtend() {
            return PartialView("_VerplegingOverzichtPatientenWachtend", new Models.VM.VerplegingOverzicht.PatientenTeVerplaatsenVM());
        }
        #endregion VerplegingOverzicht

        #region MaakVervoerAanvraag
        [HttpGet]
        public ViewResult MaakVervoerAanvraag() {
            return View(new Models.VM.MaakVervoerAanvraag.AanvraagTypesVM());
        }

        /// <summary>
        /// Het maken van een Aanvraag
        /// </summary>
        [HttpGet]
        public PartialViewResult GetMaakVervoerAanvraag_AanvraagDetails(string aanvraagTypeId) {
            return PartialView("_MaakVervoerAanvraag_AanvraagDetails", new Models.VM.MaakVervoerAanvraag.MaakAanvraag(aanvraagTypeId));
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

                if (saveAanvraag.AanvraagType.Include_Patient || saveAanvraag.AanvraagType.Include_PatientVisit) {
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
                return View();
            }
            return RedirectToAction("MaakVervoerAanvraag");
        }
        #endregion MaakVervoerAanvraag
    }
}