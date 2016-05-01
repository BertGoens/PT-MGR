using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    public class TransportTaak {
        public TransportTaak() {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Van")]
        // TODO Later Locatie Tabel MaxLength toevoegen
        // TODO Later Locatietabel linken
        [MaxLength(255)]
        public string LocatieStart { get; set; }

        [Display(Name = "Naar")]
        // TODO Later MaxLength toevoegen
        // TODO Later Locatietabel linken
        [MaxLength(255)]
        public string LocatieEind { get; set; }

        [Display(Name = "Hoge prioriteit")]
        public bool IsPrioriteitHoog { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum gemaakt")]
        public DateTime DatumGemaakt
        {
            get
            {
                return this.DatumGemaakt != null
                   ? this.DatumGemaakt
                   : DateTime.Now;
            }

            set { this.DatumGemaakt = value; }
        }

        [DisplayFormat(DataFormatString = "{0:mm:ss}")]
        public DateTime TijdNodig { get; set; }

        [DisplayFormat(DataFormatString = "{0:mm:ss}")]
        public DateTime GeschatteTijdNodig { get; set; }
        
        public long AanvraagId { get; set; }
        [ForeignKey("AanvraagId")]
        public virtual Aanvraag Aanvraag { get; set; }

        [MaxLength(255)]
        public string ToegewezenWerknemerId { get; set; }
        [ForeignKey("ToegewezenWerknemerId")]
        public TransportWerknemer ToegewezenWerknemer { get; set; }
    }
}