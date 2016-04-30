using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Patient_Transport_Migration.Models.DAL {

    [MetadataType(typeof(DokterMetaData))]
    public partial class Dokter {
        public string Name { get; set; }
        public int ID { get; set; }
        public bool ConsultOpen { get; set; }
    }

    public class DokterMetaData {
        [MinLength(5), MaxLength(255)]
        [Display(Name="Dokter")]
        [Required]
        public string Name { get; set; }

        [Key]
        [HiddenInput(DisplayValue=false)]
        public int ID { get; set; }

        [Display(Name = "Open voor consult")]
        public bool ConsultOpen { get; set; }
    }
}