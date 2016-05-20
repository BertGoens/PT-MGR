using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    /* TODO Remove AanvraagStatus
    public class AanvraagStatus {
        public AanvraagStatus() {

        }

        // Composite Key
        // FK 1
        [Key]
        [Column(Order = 1)]
        public int AanvraagTypeId { get; set; }
        [ForeignKey("AanvraagTypeId")]
        public virtual AanvraagType AanvraagType { get; set; }

        // FK 2
        [Key]
        [Column(Order = 2)]
        public int StapNr { get; set; }

        [MaxLength(255)]
        public string Omschrijving { get; set; }

        public bool IsLaatsteStap { get; set; }
    }
    */
}