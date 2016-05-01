
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    // Al deze data komt uit een view
    public partial class Patient {
        public Patient() {

        }

        [Key]
        [Column(Order = 1)]
        [MaxLength(10)]
        public string Id { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(10)]
        public string PatientVisit { get; set; }

        [MaxLength(255)]
        public string Voornaam { get; set; }

        [MaxLength(255)]
        public string Achternaam { get; set; }

        [Display(Name = "Naam:")]
        public string Naam { get { return Voornaam + " " + Achternaam; } }

        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }

        public Geslacht Geslacht { get; set; }

        [MaxLength(10)]
        public string Afdeling { get; set; }

        [MaxLength(10)]
        public string Kamer { get; set; }

        [MaxLength(10)]
        [Display(Name = "Bed Nr")]
        public string BedNr { get; set; }

        public string BehandelendeDokterId { get; set; }
        [Display(Name = "Behandelende dokter:")]
        [ForeignKey("BehandelendeDokterId")]
        public Dokter BehandelendeDokter { get; set; }
    }
}