﻿using System.ComponentModel.DataAnnotations;

namespace Patient_Transport_Migration.Models.POCO {
    public class TransportWerknemer {
        // PK
        [Key]
        [MaxLength(255)]
        public string Gebruikersnaam { get; set; }

        [MaxLength(255)]
        public string Voornaam { get; set; }

        [MaxLength(255)]
        public string Achternaam { get; set; }

        public string Naam { get { return Voornaam + " " + Achternaam; } }

    }
}