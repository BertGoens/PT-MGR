using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Patient_Transport_Migration.Models.POCO {

    public class Dokter {
        public Dokter() {
        }

        [Key]
        [MaxLength(10)]
        public string Id { get; set; }

        [MaxLength(255)]
        public string Naam { get; set; }

        [Display(Name = "Verwacht consult")]
        public bool IsConsultVerwachtend { get; set; }

        public Locatie Locatie { get; set; }

        /// <summary>
        /// De Domain User die wordt gebruikt om de taken te beheren.
        /// </summary>
        [MaxLength(255)]
        public string GebruikersNaam { get; set; }
    }

}