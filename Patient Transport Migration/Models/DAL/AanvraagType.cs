using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    public class AanvraagType {
        public AanvraagType() {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Omschrijving { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DatumGemaakt { get; set; }

        /* {N} Data-velden; allemaal nullable */
        // Gedeelde velden
        [DefaultValue(false)]
        public bool Include_Patient { get; set; }
        [DefaultValue(false)]
        public bool Include_PatientVisit { get; set; }

        // Emerged Request: 'er' prefix
        [DefaultValue(false)]
        public bool Include_er_Omschrijving { get; set; }

        // Aanvraag van Consult: 'avc' prefix
        [DefaultValue(false)]
        public bool Include_avc_AanDokter { get; set; }

        [DefaultValue(false)]
        public bool Include_avc_PatientWordtBehandeldVoor { get; set; }
        [DefaultValue(false)]
        public bool Include_avc_HuidigeKlachten { get; set; }
        [DefaultValue(false)]
        public bool Include_avc_UwAdviesGevraagdVoor { get; set; }
        [DefaultValue(false)]
        public bool Include_avc_AndereNotas { get; set; }

        [DefaultValue(false)]
        public bool Include_avc_AanvragendeGeneesheer { get; set; }

        [DefaultValue(false)]
        public bool Include_avc_BevindingenEnAdvies { get; set; }
        [DefaultValue(false)]
        public bool Include_avc_DatumBevindingen { get; set; }

        // Aanvraag van Radiologie: 'avr' prefix
        [DefaultValue(false)]
        public bool Include_avr_RelevanteKlinischeInlichtingen { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_DiagnostischeVraagstelling { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_VoorgesteldeOnderzoeken { get; set; }

        [DefaultValue(false)]
        public bool Include_avr_CT { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_NMR { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_RX { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_Echografie { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_Andere { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_Onbekend { get; set; }

        [DefaultValue(false)]
        public bool Include_avr_Transportwijze { get; set; }

        [DefaultValue(false)]
        public bool Include_avr_Allergieen { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_IsDiabeet { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_HeeftNierInsufficientie { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_IsZwanger { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_HeeftImplantaat { get; set; }
        [DefaultValue(false)]
        public bool Include_avr_AndereInlichtingen { get; set; }
    }
}