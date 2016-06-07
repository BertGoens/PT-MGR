
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    public class Patient {
        public Patient() {
        }

        [Key]
        [Column(Order = 1)]
        [MaxLength(10)]
        public string PatientId { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(10)]
        public string PatientVisit { get; set; }

        [MaxLength(255)]
        public string Voornaam { get; set; }

        [MaxLength(255)]
        public string Achternaam { get; set; }

        [Display(Name = "Patient")]
        public string Naam() {
            return Voornaam + " " + Achternaam;
        }

        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }

        public Geslacht Geslacht { get; set; }

        /// <summary>
        /// De Code van de afdeling waar de patient ligt.
        /// </summary>
        [MaxLength(6)]
        public string Afdeling { get; set; }
        [MaxLength(10)]
        public string Kamer { get; set; }
        [ForeignKey("Kamer, Afdeling")]
        public virtual Locatie Locatie { get; set; }

        [MaxLength(10)]
        [Display(Name = "Bed #")]
        public string BedNr { get; set; }
    }
}