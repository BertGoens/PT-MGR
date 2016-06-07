using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Patient_Transport_Migration.Models.DAL {
    public class Locatie {
        public Locatie() {
        }

        [Key]
        [Column(Order = 1)]
        [MaxLength(10)]
        public string Kamer { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(6)]
        public string Afdeling { get; set; }

        [MaxLength(50)]
        public string Omschrijving { get; set; }
    }
}