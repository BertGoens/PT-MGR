using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    public class TransportWerknemerTaak {
        // Composite Key
        // 1 FK als PK
        [Key]
        [Column(Order = 1)]
        [MaxLength(255)]
        public string GebruikersnaamId { get; set; }
        [ForeignKey("GebruikersnaamId")]
        public virtual TransportWerknemer Gebruikersnaam { get; set; }

        // 2 FK als PK
        [Key]
        [Column(Order = 2)]
        public int TransportTaakId { get; set; }
        [ForeignKey("TransportTaakId")]
        public virtual TransportTaak TransportTaak { get; set; }

        public int TaakWachtrijNummer { get; set; }

        public bool IsActieveTaak { get { return this.TaakWachtrijNummer == 0; } }
    }
}