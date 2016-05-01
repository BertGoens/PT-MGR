using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM {
    public class DokterStatusVM {

        public DokterStatusVM() { }

        public static DokterStatusVM Create(HttpCookie DokterIdCookie = null) {
            var vm = new DokterStatusVM();
            var db = new MSSQLContext();

            //Query db
            var doctorList = db.tblDokters.Where(d => d.IsConsultDokter == true).ToList();
            //Sort A-Z
            doctorList.OrderBy(doctor => doctor.Naam);
            //Include in VM
            vm.DokterLijst = new SelectList(doctorList, "Id", "Naam");

            if (DokterIdCookie != null) {
                //find docter with the saved id
                var dokId = DokterIdCookie.Value;
                try {
                    //find doctor in db
                    vm.DokterDetailsVM = DokterDetailsVM.Create(dokId);

                    //set standard selected on this doctor (we need to create a new list)
                    var indexOfDoctorInList = doctorList.IndexOf(vm.DokterDetailsVM.Dokter);
                    indexOfDoctorInList++; // Add 1 because we insert a description on item 0 in the view
                    vm.DokterLijstSelected = vm.DokterDetailsVM.Dokter.Id;
                    vm.DokterLijst = new SelectList(doctorList, "Id", "Naam", indexOfDoctorInList);

                } catch (Exception) {
                    throw;
                }
            } else {
                vm.DokterDetailsVM = DokterDetailsVM.Create();
            }

            return vm;
        }

        public DokterDetailsVM DokterDetailsVM { get; set; }

        public string DokterLijstSelected { get; set; }
        public SelectList DokterLijst { get; set; }
    }

    // Child Model
    public class DokterDetailsVM {
        public DokterDetailsVM() { }

        public static DokterDetailsVM Create(string DokterId = null) {
            var vm = new DokterDetailsVM();
            var db = new MSSQLContext();

            if (!string.IsNullOrEmpty(DokterId)) {
                // Zoek dokter op basis van PK
                var dbEntry = db.tblDokters.Where(d => d.Id.Equals(DokterId)).First();
                if (dbEntry.IsConsultDokter) {
                    vm.Dokter = dbEntry;
                }
            }

            return vm;
        }

        public Dokter Dokter { get; set; }
    }
}