using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    // TODO  (2) TransportTaak
    // Later Locatie Tabel Linken
    // Later Geschatte tijd berekenen ipv statische 8 minuten
    public class TransportTaak {
        public TransportTaak() {
            this.GeschatteTijdNodigInSeconden = 480;
            this.DatumGemaakt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Van")]
        [MaxLength(255)]
        public string LocatieStart { get; set; }

        [Display(Name = "Naar")]
        [MaxLength(255)]
        public string LocatieEind { get; set; }

        /// <summary>
        /// Notities gedeeld tussen Dispatch(Update) & TransportWerknemer(Read)
        /// </summary>
        [MaxLength(500)]
        [Display(Name = "Notities")]
        public string TransportNotities { get; set; }

        [Display(Name = "Hoge prioriteit")]
        public bool IsPrioriteitHoog { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum gemaakt")]
        public DateTime DatumGemaakt { get; set; }

        /// <summary>
        /// De absolute tijd, in seconden, die de taak nodig had om voltooid te worden.
        /// </summary>
        public int? TijdNodigInSeconden { get; set; }

        /// <summary>
        /// De geschatte tijd,in seconden, nodig om de taak te voltooien.
        /// </summary>
        public int? GeschatteTijdNodigInSeconden { get; set; }
        
        public long AanvraagId { get; set; }
        [ForeignKey("AanvraagId")]
        public virtual Aanvraag Aanvraag { get; set; }

        [MaxLength(255)]
        public string TransportWerknemerTaakId { get; set; }
        [ForeignKey("TransportWerknemerTaakId")]
        public TransportWerknemerTaak TransportWerknemerTaak { get; set; }

        /// <summary>
        /// De (berekende) status van de taak.
        /// </summary>
        public TransportTaakStatus GetTransportTaakStatus() {
            if (this.TijdNodigInSeconden != null) {
                return TransportTaakStatus.Voltooid;
            }

            if (!string.IsNullOrEmpty(this.TransportWerknemerTaakId)) {
                if (this.TransportWerknemerTaak.IsActieveTaak()) {
                    return TransportTaakStatus.WerknemerToegewezen_HuidigeTaak;
                }

                return TransportTaakStatus.WerknemerToegewezen_Wachtend;
            }

            if (this.GeschatteTijdNodigInSeconden != null) {
                return TransportTaakStatus.TransportTaakTijdNodigGeschat;
            }

            return TransportTaakStatus.TransportTaakGemaakt;
        }
    }

    /// <summary>
    /// Transport Taak Status
    /// </summary>
    public enum TransportTaakStatus {
        TransportTaakGemaakt,
        TransportTaakTijdNodigGeschat,
        WerknemerToegewezen_Wachtend,
        WerknemerToegewezen_HuidigeTaak,
        Voltooid
    }
}