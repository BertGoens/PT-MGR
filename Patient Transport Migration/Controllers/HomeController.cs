using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.VM;

namespace Patient_Transport_Migration.Controllers {
    [HandleError]
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

        [HttpGet]
        public ActionResult DokterStatus() {
            HttpCookie doctorCookie = Request.Cookies["dokterId"];
            var vm = DokterStatusVM.Create(doctorCookie);
            return View(vm);
        }

        [HttpGet]
        public PartialViewResult GetDokterDetails(string docterId) {
            var vm = new DokterStatusVM();
            vm.DokterDetailsVM = DokterDetailsVM.Create(docterId);
            return PartialView("_EditDokter", vm);
        }

        [HttpPost]
        public ActionResult DokterStatus(DokterStatusVM viewModel) {
            if (ModelState.IsValid) {
                //Save chosen doctor to cookie
                var docId = viewModel.DokterLijstSelected;
                var dokterIdCookie = new HttpCookie("dokterId", docId.ToString());
                dokterIdCookie.Expires.AddDays(365);
                Response.SetCookie(dokterIdCookie);

                //Save doctor consult status
                db.Entry(viewModel.DokterDetailsVM.Dokter).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DokterStatus");
            } else 
           {
                var errors = getViewModelErrors();
                ViewBag.ErrorMessage = errors;
            }

            return View();
        }


        [HttpGet]
        public ActionResult PatientInfo() {
            var pivm = new PatientInfoVM();

            //Query db for unique patients
            var patientEntries = db.tblPatienten.ToList();
            //Query patients with unique numbers

            //Sort A-Z
            patientEntries.OrderBy(patient => patient.Achternaam);
            //Add to VM
            pivm.PatientenLijst = new SelectList(patientEntries, "PatientVisit", "Naam"); //(Items / DataValueField / DataTextField)

            return View(pivm);
        }

        [HttpGet]
        public PartialViewResult GetSelectedPatient(string visitId) {
            var vm = new PatientInfoVM();

            if (!string.IsNullOrEmpty(visitId) && visitId.Length > 0) {
                // Get the patient for his details
                vm.PatientDetails = db.tblPatienten.Where(p => p.PatientVisit.Equals(visitId)).First();
                // Get all the requests including said patient
                // TODO Patient Transport Requests filteren: huidige visit, alleen taken met Include_Patient
                var error = db.tblTransportTaken.Where(task => task.Aanvraag.PatientVisit.Equals(visitId)).ToList();
                vm.PatientRequests = error;
            }

            return PartialView("_PatientDetails", vm);
        }

        [HttpGet]
        //[ChildActionOnly]
        public PartialViewResult GetRequestTypes() {
            var vm = new PatientInfoVM();

            //Query db for request types
            var reqList = db.tblAanvraagTypes.ToList();
            //Include in VM
            vm.RequestTypes = new SelectList(reqList, "Id", "Omschrijving");

            return PartialView("_RequestTypes", vm);
        }

        [HttpGet]
        public ActionResult VerplegingOverzicht() {
            var vm = new VerplegingOverzichtVM();
            return View(vm);
        }

        [HttpGet]
        public PartialViewResult GetPatientenInOrde() {
            var vm = new VerplegingOverzichtVM();
            // do database query
            return PartialView("_PatientenInOrde", vm);
        }

        [HttpGet]
        public PartialViewResult refreshPatientenToDo() {
            var vm = new VerplegingOverzichtVM();
            //db query
            return PartialView("_PatientenToDo", vm);
        }
    }
}