using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    public class TransportWerknemerTaak {
        // Composite Key
        // 1 FK als PK
        [Key]
        [Column(Order = 1)]
        [MaxLength(255)]
        public string TransportWerknemerId { get; set; }
        [ForeignKey("TransportWerknemerId")]
        public virtual TransportWerknemer TransportWerknemer { get; set; }

        // 2 FK als PK
        [Key]
        [Column(Order = 2)]
        public int TransportTaakId { get; set; }
        [ForeignKey("TransportTaakId")]
        public virtual TransportTaak TransportTaak { get; set; }

        public int TaakWachtrijNummer { get; set; }

        /// <summary>
        /// Bekijkt of de taak is toegewezen, en of hij #1 is
        /// </summary>
        /// <returns></returns>
        public bool IsActieveTaak() {
            if (!string.IsNullOrEmpty(TransportWerknemerId)) {
                if (TaakWachtrijNummer == 0) {
                    return true;
                }
            }
            return false;
        }
    }
}