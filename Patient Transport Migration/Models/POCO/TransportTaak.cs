﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.POCO {
    public class TransportTaak {
        public TransportTaak() {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [MaxLength(10)]
        public string LocatieStartKamer { get; set; }
        [MaxLength(6)]
        public string LocatieStartAfdelingId { get; set; }
        [ForeignKey("LocatieStartKamer, LocatieStartAfdelingId")]
        public virtual Locatie LocatieStart { get; set; }

        [MaxLength(10)]
        public string LocatieEindKamer { get; set; }
        [MaxLength(6)]
        public string LocatieEindAfdelingId { get; set; }
        [ForeignKey("LocatieEindKamer, LocatieEindAfdelingId")]
        public virtual Locatie LocatieEind { get; set; }


        [MaxLength(10)]
        public string DokterId { get; set; }
        [ForeignKey("DokterId")]
        public virtual Dokter DokterEind { get; set; }

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
        public DateTime? DatumGemaakt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="Datum compleet")]
        public DateTime? DatumCompleet { get; set; }

        public long AanvraagId { get; set; }
        [ForeignKey("AanvraagId")]
        public virtual Aanvraag Aanvraag { get; set; }

        [DefaultValue(false)]
        public bool TransportGestart { get; set; }

        /// <summary>
        /// Het wachtrij-nummer van de taak.
        /// </summary>
        /// <value>
        /// DatumCompleet = Voltooid
        /// Null && !DatumCompleet = Niet toegewezen
        /// 0 = actieve taak
        /// > 0 = volgende taken
        /// </value>
        public int? TaakWachtrijNummer { get; set; }

        [MaxLength(255)]
        public string TransportWerknemerId { get; set; }
        /// <summary>
        /// De toegewezen werknemer voor de taak.
        /// </summary>
        [ForeignKey("TransportWerknemerId")]
        public virtual TransportWerknemer TransportWerknemer { get; set; }

    }
}
