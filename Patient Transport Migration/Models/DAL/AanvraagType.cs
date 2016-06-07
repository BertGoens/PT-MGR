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

        [Display(Name = "Aanvraag Type")]
        [MaxLength(255)]
        public string Omschrijving { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DatumGemaakt { get; set; }

        [DefaultValue(false)]
        public bool Include_Transportwijze { get; set; }

        [DefaultValue(false)]
        public bool Include_Patient { get; set; }

        // Aanvraag van Consult
        [DefaultValue(false)]
        public bool Include_AanDokter { get; set; }

        // Aanvraag van Radiologie
        [DefaultValue(false)]
        public bool Include_Radiologie { get; set; }
        
    }
}