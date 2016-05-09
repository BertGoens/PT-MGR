using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    public class Aanvraag {
        public Aanvraag() {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int AanvraagTypeId { get; set; }
        [ForeignKey("AanvraagTypeId")]
        public virtual AanvraagType AanvraagType { get; set; }
       
        public int AanvraagStatusId { get; set; }
        public int AanvraagStapNr { get; set; }
        [ForeignKey("AanvraagStatusId, AanvraagStapNr")]
        public virtual AanvraagStatus AanvraagStatus { get; set; }

        /// <summary>
        /// De gebruikersnaam van de gebruiker die de vervoersaanvraag plaatste.
        /// </summary>
        [MaxLength(255)]
        public string AanvraagDoor { get; set; }

        /* {N} Data-velden; allemaal nullable */
        // Gedeeld voor meerdere requests

        [MaxLength(10)]
        public string PatientId { get; set; }

        [MaxLength(10)]
        public string PatientVisit { get; set; }

        [ForeignKey("PatientId, PatientVisit")]
        public virtual Patient Patient { get; set; }

        // Vervoer Aanvraag: 'va' prefix
        [MaxLength(1000)]
        public string va_Omschrijving { get; set; }

        // Aanvraag van Consult: 'avc' prefix
        [MaxLength(10)]
        public string avc_AanDokterId { get; set; }
        [ForeignKey("avc_AanDokterId")]
        public virtual Dokter avc_AanDokter { get; set; }

        [MaxLength(1000)]
        public string avc_PatientWordtBehandeldVoor { get; set; }
        [MaxLength(1000)]
        public string avc_HuidigeKlachten { get; set; }
        [MaxLength(1000)]
        public string avc_UwAdviesGevraagdVoor { get; set; }
        [MaxLength(1000)]
        public string avc_AndereNotas { get; set; }

        [MaxLength(10)]
        public string avc_AanvragendeGeneesheerId { get; set; }
        [ForeignKey("avc_AanvragendeGeneesheerId")]
        public Dokter avc_AanvragendeGeneesheer { get; set; }

        [MaxLength(1000)]
        public string avc_BevindingenEnAdvies { get; set; }

        [DataType(DataType.Date)]
        public DateTime? avc_DatumBevindingen { get; set; }

        // Aanvraag van Radiologie: 'avr' prefix
        [MaxLength(1000)]
        public string avr_RelevanteKlinischeInlichtingen { get; set; }
        [MaxLength(1000)]
        public string avr_DiagnostischeVraagstelling { get; set; }
        [MaxLength(1000)]
        public string avr_VoorgesteldeOnderzoeken { get; set; }

        public bool avr_CT { get; set; }
        public bool avr_NMR { get; set; }
        public bool avr_RX { get; set; }
        public bool avr_Echografie { get; set; }
        [MaxLength(255)]
        public string avr_Andere { get; set; }
        public bool avr_Onbekend { get; set; }

        public int avr_TransportwijzeId { get; set; }
        [ForeignKey("avr_TransportwijzeId")]
        public Transportwijze avr_Transportwijze { get; set; }

        [MaxLength(1000)]
        public string avr_Allergieen { get; set; }
        public bool avr_IsDiabeet { get; set; }
        public bool avr_HeeftNierInsufficientie { get; set; }
        public bool avr_IsZwanger { get; set; }
        public bool avr_HeeftImplantaat { get; set; }
        [MaxLength(1000)]
        public string avr_AndereInlichtingen { get; set; }
    }
}