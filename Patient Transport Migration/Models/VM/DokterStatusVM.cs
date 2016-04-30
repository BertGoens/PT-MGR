using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM {
    public class DokterStatusVM {

        public Dokter Doctor { get; set; }

        public int DoctorListId { get; set; }
        public SelectList DoctorList { get; set; }
    }
}