using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    [MetadataType(typeof(DiagnostischeOnderzoekenMetaData))]
    public partial class DiagnostischeOnderzoeken {     
        public int Pk_Id { get; set; }
        public string Patient { get; set; }
        public bool? isCt { get; set; }
        public bool? isNmr { get; set; }
        public bool? isRx { get; set; }
        public bool? isEchografie { get; set; }
        public bool? isOnbekend { get; set; }
        public string Andere { get; set; }
        public string Bevindingen { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_complete { get; set; }
    }

    [Table("Diagnostische_Onderzoeken")]
    public class DiagnostischeOnderzoekenMetaData {

        [Display(Name = "Id")]
        [Column("Pk_Id")]
        [Key]
        public int Pk_Id { get; set; }

        [Display(Name = "Patient")]
        [Column("patient")]
        [StringLength(10)]
        public string Patient { get; set; }

        [Display(Name = "CT", GroupName = "Onderzoeken ivm. de diagnostische vraagstelling")]
        [Column("ct")]
        public bool? isCt { get; set; }

        [Display(Name = "NMR", GroupName = "Onderzoeken ivm. de diagnostische vraagstelling")]
        [Column("nmr")]
        public bool? isNmr { get; set; }

        [Display(Name = "RX", GroupName = "Onderzoeken ivm. de diagnostische vraagstelling")]
        [Column("rx")]
        public bool? isRx { get; set; }

        [Display(Name = "Echografie", GroupName = "Onderzoeken ivm. de diagnostische vraagstelling")]
        [Column("echografie")]
        public bool? isEchografie { get; set; }

        [Display(Name = "Onbekend", GroupName = "Onderzoeken ivm. de diagnostische vraagstelling")]
        [Column("onbekend")]
        public bool? isOnbekend { get; set; }

        [Display(Name = "Andere", GroupName = "Onderzoeken ivm. de diagnostische vraagstelling")]
        [Column("andere")]
        [StringLength(255)]
        public string Andere { get; set; }

        [Display(Name = "Bevindingen")]
        [Column("bevindingen")]
        [StringLength(255)]
        public string Bevindingen { get; set; }

        [Display(Name = "Datum toegevoegd")]
        [Column("date_created")]
        public DateTime? date_created { get; set; }

        [Display(Name = "Datum afgenomen")]
        [Column("date_complete")]
        public DateTime? date_complete { get; set; }

    }
}