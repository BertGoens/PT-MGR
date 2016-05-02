using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models;
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
            return PartialView("_DokterStatusEditDokter", vm);
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
            var vm = PatientInfoVM.Create();
            return View(vm);
        }

        [HttpGet]
        public PartialViewResult GetPatientDetails(string visitId) {
            var vm = new PatientInfoVM();
            vm.PatientDetailsVM = PatientDetailsVM.Create(visitId);
            return PartialView("_PatientInfoPatientDetails", vm);
        }

        [HttpGet]
        public PartialViewResult GetPatientMedischeAanvragen(string visitId) {
            var vm = new PatientInfoVM();
            vm.PatientMedischeAanvragenVM = PatientMedischeAanvragenVM.Create(visitId);
            return PartialView("_PatientInfoPatientMedischeAanvragen", vm);
        }

        [HttpGet]
        public PartialViewResult GetAanvraagTypes() {
            var vm = new PatientInfoVM();
            vm.AanvraagTypesVM = AanvraagTypesVM.Create();
            return PartialView("_PatientInfoAanvraagTypes", vm);
        }

        // TODO PatientInfo_PatientAanvraagDetails(aanvraagId)

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