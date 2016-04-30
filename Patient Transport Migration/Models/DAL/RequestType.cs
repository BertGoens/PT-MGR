using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Web.Mvc;

namespace Patient_Transport_Migration.Models.DAL {

    [MetadataType(typeof(RequestTypeMetaData))]
    public partial class RequestType {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        /* Data velden */
        public bool Include_Patient { get; set; }
        public bool Include_datum_van_aanvraag { get; set; }
        public bool Include_datum_behandeld { get; set; }
        public bool Include_huidige_klachten { get; set; }
        public bool Include_aan_dokter { get; set; }
        public bool Include_andere_notas { get; set; }
        public bool Include_aanvragende_geneesheer { get; set; }
        public bool Include_patient_wordt_behandeld_voor { get; set; }
        public bool Include_bevindingen_en_advies { get; set; }
        public bool Include_relevante_klinische_inlichtingen { get; set; }
        public bool Include_diagnostische_vraagstelling { get; set; }
        public bool Include_transport_te_voet { get; set; }
        public bool Include_transport_rolstoel { get; set; }
        public bool Include_transport_bed { get; set; }
        public bool Include_zwanger { get; set; }
        public bool Include_diabeet { get; set; }
        public bool Include_implantaat { get; set; }
        public bool Include_nierinsufficientie { get; set; }
        public bool Include_allergie { get; set; }
        public bool Include_andere { get; set; }
    }

    [Table("Request_Type")]
    public class RequestTypeMetaData {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Beschrijving")]
        [StringLength(255)]
        [Column("description")]
        public string Description { get; set; }

        [Display(Name = "Datum toegevoegd")]
        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }

        /* Data velden */
        [Display(Name = "Heeft patiënt")]
        public bool Include_Patient { get; set; }

        [Display(Name = "Datum van aanvraag")]
        [Column("datum_van_aanvraag")]
        public bool Include_datum_van_aanvraag { get; set; }

        [Display(Name = "Datum behandeld")]
        [Column("datum_behandeld")]
        public bool Include_datum_behandeld { get; set; }

        [Display(Name = "Huidige klachten")]
        [Column("huidige_klachten")]
        public bool Include_huidige_klachten { get; set; }

        [Display(Name = "Aan dokter")]
        [Column("aan_dokter")]
        public bool Include_aan_dokter { get; set; }

        [Display(Name = "Andere nota's")]
        [Column("andere_notas")]
        public bool Include_andere_notas { get; set; }

        [Display(Name = "Aanvragende geneesheer")]
        [Column("aanvragende_geneesheer")]
        public bool Include_aanvragende_geneesheer { get; set; }

        [Display(Name = "Patient wordt behandeld voor")]
        [Column("patient_wordt_behandeld_voor")]
        public bool Include_patient_wordt_behandeld_voor { get; set; }

        [Display(Name = "Bevindingen en advies")]
        [Column("bevindingen_en_advies")]
        public bool Include_bevindingen_en_advies { get; set; }

        [Display(Name = "Relevante klinische inlichtingen")]
        [Column("relevante_klinische_inlichtingen")]
        public bool Include_relevante_klinische_inlichtingen { get; set; }

        [Display(Name = "Diagnostische vraagstelling")]
        [Column("diagnostische_vraagstelling")]
        public bool Include_diagnostische_vraagstelling { get; set; }

        [Display(Name = "Te voet", GroupName = "Transportwijze")]
        [Column("transport_te_voet")]
        public bool Include_transport_te_voet { get; set; }

        [Display(Name = "Rolstoel", GroupName = "Transportwijze")]
        [Column("transport_rolstoel")]
        public bool Include_transport_rolstoel { get; set; }

        [Display(Name = "Bed", GroupName = "Transportwijze")]
        [Column("transport_bed")]
        public bool Include_transport_bed { get; set; }

        [Column("zwanger")]
        [Display(Name = "Is zwanger", GroupName = "Relevante bijkomende inlichtingen")]
        public bool Include_zwanger { get; set; }

        [Column("diabeet")]
        [Display(Name = "Is diabeet", GroupName = "Relevante bijkomende inlichtingen")]
        public bool Include_diabeet { get; set; }

        [Column("implantaat")]
        [Display(Name = "Heeft implantaat", GroupName = "Relevante bijkomende inlichtingen")]
        public bool Include_implantaat { get; set; }

        [Column("nierinsufficientie")]
        [Display(Name = "Heeft nierinsufficientie", GroupName = "Relevante bijkomende inlichtingen")]
        public bool Include_nierinsufficientie { get; set; }

        [Column("allergie")]
        [Display(Name = "Heeft allergie", GroupName = "Relevante bijkomende inlichtingen")]
        public bool Include_allergie { get; set; }

        [Column("andere")]
        [Display(Name = "Andere", GroupName = "Relevante bijkomende inlichtingen")]
        public bool Include_andere { get; set; }
    }
}