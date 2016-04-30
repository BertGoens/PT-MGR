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
    public class HomeController : Controller {
        private MSSQLContext db = new MSSQLContext();

        [HttpGet]
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult DokterStatus() {
            var dsvm = new DokterStatusVM();

            //Query db for doctors
            var doctorList = db.Docters.ToList();
            //Sort A-Z
            doctorList.OrderBy(doctor => doctor.Name);
            //Include in VM
            dsvm.DoctorList = new SelectList(doctorList, "ID", "Name");

            //Get the persistent cookie if it exists
            var doctorCookie = Request.Cookies["dokterId"] as HttpCookie;
            if (doctorCookie != null) {
                //find docter with the saved id
                try {
                    //get the id
                    int docterId = Convert.ToInt32(doctorCookie.Value);
                    //find doctor in db
                    dsvm.Doctor = (Dokter)db.Docters.First(dx => dx.ID == docterId);

                    //set standard selected on this doctor (we need to create a new list)
                    var indexOfDoctorInList = doctorList.IndexOf(dsvm.Doctor);
                    // Add 1 because we insert a description on item 0 in the view
                    indexOfDoctorInList++;
                    dsvm.DoctorListId = indexOfDoctorInList;
                    //Add SelectList to VM
                    dsvm.DoctorList = new SelectList(doctorList, "ID", "Name", indexOfDoctorInList);

                } catch (Exception) {
                    //Cookie aangepast door de user of de entry bestaat niet meer in de db
                    throw;
                }
            }

            return View(dsvm);
        }

        [HttpGet]
        public PartialViewResult GetDokterDetails(string docterId) {
            int id;
            var vm = new DokterStatusVM();
            if (int.TryParse(docterId, out id)) {
                vm.Doctor = db.Docters.Where(d => d.ID == id).First();
            }
            return PartialView("_EditDokter", vm);
        }

        [HttpPost]
        public ActionResult DokterStatus(DokterStatusVM viewModel) {
            if (ModelState.IsValid) {
                //Save chosen doctor to cookie
                var docId = viewModel.DoctorListId;
                var dokterIdCookie = new HttpCookie("dokterId", docId.ToString());
                dokterIdCookie.Expires.AddDays(365);
                Response.SetCookie(dokterIdCookie);

                //Save doctor consult status
                db.Entry(viewModel.Doctor).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DokterStatus");
            } else {
                var errors = string.Join(",", ModelState.Values.Where(e => e.Errors.Count > 0)
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray());
                errors.ToString();
                ViewBag.ErrorMessage = errors;
            }

            return View();
        }


        [HttpGet]
        public ActionResult PatientInfo() {
            var pivm = new PatientInfoVM();

            //Query db for unique patients
            var patientEntries = db.Patients.ToList();
            //Query patients with unique numbers

            //Sort A-Z
            patientEntries.OrderBy(patient => patient.LastName);
            //Add to VM
            pivm.Patients = new SelectList(patientEntries, "VisitId", "Name"); //(Items / DataValueField / DataTextField)

            return View(pivm);
        }

        [HttpGet]
        public PartialViewResult GetSelectedPatient(string visitId) {
            var vm = new PatientInfoVM();

            if (!string.IsNullOrEmpty(visitId) && visitId.Length > 0) {
                // Get the patient for his details
                vm.PatientDetails = db.Patients.Where(p => p.VisitId.Equals(visitId)).First();
                // Get all the requests including said patient
                // TODO! Patient Transport Requests filteren: huidige visit, alleen taken met Include_Patient
                var error = db.TransportTasks.Where(task => task.PatientVisit.Equals(visitId)).ToList();
                vm.PatientRequests = error;
            }

            return PartialView("_PatientDetails", vm);
        }

        [HttpGet]
        //[ChildActionOnly]
        public PartialViewResult GetRequestTypes() {
            var vm = new PatientInfoVM();

            //Query db for request types
            var reqList = db.RequestTypes.ToList();
            //Include in VM
            vm.RequestTypes = new SelectList(reqList, "Id", "Description");

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