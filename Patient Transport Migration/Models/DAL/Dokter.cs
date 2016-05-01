using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Patient_Transport_Migration.Models.DAL {

    public partial class Dokter {
        public Dokter() {

        }

        [Key]
        [MaxLength(10)]
        public string Id { get; set; }

        [MaxLength(255)]
        public string Naam { get; set; }

        [Display(Name = "Is consult dokter")]
        public bool IsConsultDokter { get; set; }

        [Display(Name = "Verwacht consult")]
        public bool IsConsultVerwachtend { get; set; }
    }

}