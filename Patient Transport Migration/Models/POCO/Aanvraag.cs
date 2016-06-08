using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.POCO {
    public class Aanvraag {
        public Aanvraag() {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Aanvraag #")]
        public long? Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Aanvraag voor")]
        public DateTime DatumAanvraag { get; set; }

        [Display(Name = "Aanvraag gedaan op")]
        [DataType(DataType.DateTime)]
        public DateTime? DatumCompleet { get; set; }

        public int AanvraagTypeId { get; set; }
        [ForeignKey("AanvraagTypeId")]
        public virtual AanvraagType AanvraagType { get; set; }

        /// <summary>
        /// De gebruikersnaam van de gebruiker die de vervoersaanvraag plaatste.
        /// </summary>
        [Display(Name = "Aanvraag opgemaakt door")]
        [MaxLength(255)]
        public string AanvraagDoor { get; set; }

        /// <summary>
        /// Gebruikt om alle persoonlijke patientdata op te zoeken.
        /// </summary>
        [MaxLength(10)]
        public string PatientId { get; set; }

        /// <summary>
        /// Gebruikt om de huidige patient zijn bezoekdata bij te houden.
        /// </summary>
        [MaxLength(10)]
        public string PatientVisit { get; set; }

        [ForeignKey("PatientId, PatientVisit")]
        public virtual Patient Patient { get; set; }

        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Omschrijving { get; set; }

        // Aanvraag van Consult: 'avc' prefix
        [MaxLength(10)]
        public string AanDokterId { get; set; }
        [ForeignKey("AanDokterId")]
        [Display(Name = "Behandelende dokter")]
        public virtual Dokter AanDokter { get; set; }
        [DefaultValue(false)]
        public bool DokterOntslagen { get; set; }

        // Aanvraag van Radiologie: 
        [DefaultValue(false)]
        public bool CT { get; set; }
        [DefaultValue(false)]
        public bool CT_Ontslagen { get; set; }

        [DefaultValue(false)]
        public bool NMR { get; set; }
        [DefaultValue(false)]
        public bool NMR_Ontslagen { get; set; }

        [DefaultValue(false)]
        public bool RX { get; set; }
        [DefaultValue(false)]
        public bool RX_Ontslagen { get; set; }

        [DefaultValue(false)]
        public bool Echografie { get; set; }
        [DefaultValue(false)]
        public bool Echografie_Ontslagen { get; set; }

        public int? TransportwijzeId { get; set; }
        [ForeignKey("TransportwijzeId")]
        public Transportwijze Transportwijze { get; set; }

    }
}