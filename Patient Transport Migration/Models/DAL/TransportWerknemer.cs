using System.ComponentModel.DataAnnotations;

namespace Patient_Transport_Migration.Models.DAL {
    public class TransportWerknemer {
        // PK
        [Key]
        [MaxLength(255)]
        public string Gebruikersnaam { get; set; }

        [MaxLength(255)]
        public string Voornaam { get; set; }

        [MaxLength(255)]
        public string Achternaam { get; set; }

        public string Naam() {
            return Voornaam + " " + Achternaam;
        }

        /// <summary>
        /// TODO Houd de status van is werknemer present zelf bij (in DispatchOverzicht)
        /// </summary>
        public bool IsPresent { get; set; }

    }
}