﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Patient_Transport_Migration.Models.POCO {

    public class Dokter {
        public Dokter() {
        }

        [Key]
        [MaxLength(10)]
        public string Id { get; set; }

        [MaxLength(255)]
        public string Naam { get; set; }

        [Display(Name = "Verwacht consult")]
        public bool IsConsultVerwachtend { get; set; }

        public string Locatie_Kamer { get; set; }
        public string Locatie_Afdeling { get; set; }
        [ForeignKey("Locatie_Kamer, Locatie_Afdeling")]
        public virtual Locatie Locatie { get; set; }

        /// <summary>
        /// De Domain User die wordt gebruikt om de taken te beheren.
        /// </summary>
        [MaxLength(255)]
        public string GebruikersNaam { get; set; }
    }

}