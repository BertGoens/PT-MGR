
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Patient_Transport_Migration.Models.DAL {
    [MetadataType(typeof(PatientMetaData))]
    public partial class Patient {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get { return FirstName + " " + LastName; } }
        public string PatientId { get; set; }
        public string VisitId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex? Sex { get; set; }
        public Dokter DokterTreating { get; set; }
        public string RoomId { get; set; }
        public string BedId { get; set; }
    }

    public class PatientMetaData {

        [Display(Name = "Voornaam:")]
        public string FirstName { get; set; }

        [Display(Name = "Achternaam:")]
        public string LastName { get; set; }

        [Display(Name = "Naam:")]
        public string Name { get { return FirstName + " " + LastName; } }

        [Key]
        [Column(Order = 0)]
        [HiddenInput(DisplayValue = false)]
        public string PatientId { get; set; }

        [Key]
        [Column(Order = 1)]
        [HiddenInput(DisplayValue = false)]
        public string VisitId { get; set; }

        [Display(Name = "Geboortedatum:")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Geslacht: ")]
        public Sex? Sex { get; set; }

        [Display(Name = "Behandelende dokter:")]
        //Geen Foreign Key omdat we onze dokters uit een view halen en niet uit een tabel
        public Dokter DokterTreating { get; set; }

        [Display(Name = "Kamer:")]
        public string RoomId { get; set; }

        [Display(Name = "Bed nr:")]
        public string BedId { get; set; }
    }
}