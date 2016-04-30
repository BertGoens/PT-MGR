using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    [MetadataType(typeof(TransportTaskMetaData))]
    public partial class TransportTask {

        public int PK_Id { get; set; }

        public int FK_Request_Type { get; set; }
        public int? Fk_Diagnostische_Onderzoeken { get; set; }
        public int FK_Request_Status { get; set; }

        public virtual DiagnostischeOnderzoeken Diagnostische_Onderzoeken { get; set; }
        public virtual RequestStatus Request_Status { get; set; }
        public virtual RequestType Request_Type { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateComplete { get; set; }
        public string Notes { get; set; }
        public string EmployeeAssigned { get; set; }
        public bool HighPriority { get; set; }

        public string PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public string PatientVisit { get; set; }
        public string Notities { get; set; }
        public string Van { get; set; }
        public string Naar { get; set; }
        public DateTime date_created { get; set; }
        public DateTime? date_complete { get; set; }
        public DateTime? datum_van_aanvraag { get; set; }
        public DateTime? datum_behandeld { get; set; }
        public string huidige_klachten { get; set; }
        public int? aan_dokter { get; set; }
        public string andere_notas { get; set; }
        public string aanvragende_geneesheer { get; set; }
        public string patient_wordt_behandeld_voor { get; set; }
        public string bevindingen_en_advies { get; set; }
        public string relevante_klinische_inlichtingen { get; set; }
        public string diagnostische_vraagstelling { get; set; }
        public bool? transport_te_voet { get; set; }
        public bool? transport_rolstoel { get; set; }
        public bool? transport_bed { get; set; }
        public bool? zwanger { get; set; }
        public bool? diabeet { get; set; }
        public bool? implantaat { get; set; }
        public bool? nierinsufficientie { get; set; }
        public string allergie { get; set; }
        public string andere { get; set; }
    }

    internal class TransportTaskMetaData {

        [Key]
        public int PK_Id { get; set; }

        public int FK_Request_Type { get; set; }
        public int? Fk_Diagnostische_Onderzoeken { get; set; }
        public int FK_Request_Status { get; set; }
        public int? FK_Patient { get; set; }

        public virtual DiagnostischeOnderzoeken Diagnostische_Onderzoeken { get; set; }
        public virtual RequestStatus Request_Status { get; set; }
        public virtual RequestType Request_Type { get; set; }
        public virtual Patient Patient { get; set; }

        [Column("date_created")]
        public DateTime? DateCreated { get; set; }

        [Column("date_completed")]
        public DateTime? DateComplete { get; set; }

        [Display(Name = "Notities")]
        [Column("notes")]
        [StringLength(255, ErrorMessage = "Notities te lang ( > 255 tekens )")]
        public string Notes { get; set; }

        [Display(Name = "Toegewezen werknemer")]
        [Column("employee_assigned")]
        [StringLength(10)]
        public string EmployeeAssigned { get; set; }

        [Display(Name = "Hoge prioriteit")]
        [Column("high_priority")]
        public bool HighPriority { get; set; }

        [Display(Name = "Patient")]
        [Column("patient")]
        [StringLength(10)]
        public string PatientId { get; set; }

        [Display(Name = "Patient Visit")]
        [Column("patient_visit")]
        [StringLength(10)]
        public string PatientVisit { get; set; }

        [StringLength(255)]
        [Display(Name = "Notities")]
        [Column("notities")]
        public string Notities { get; set; }

        [StringLength(10)]
        [Display(Name = "Van kamer")]
        [Column("van")]
        public string Van { get; set; }

        [StringLength(10)]
        [Display(Name = "Naar kamer")]
        [Column("naar")]
        public string Naar { get; set; }

        [Display(Name = "Datum van aanvraag")]
        [Column("datum_van_aanvraag")]
        public DateTime? datum_van_aanvraag { get; set; }

        [Display(Name = "Datum behandeld")]
        [Column("datum_behandeld")]
        public DateTime? datum_behandeld { get; set; }

        [Display(Name = "Huidige klachten")]
        [Column("huidige_klachten")]
        [StringLength(255)]
        public string huidige_klachten { get; set; }

        [Display(Name = "Aan dokter")]
        [Column("aan_dokter")]
        public int? aan_dokter { get; set; }

        [Display(Name = "Andere nota's")]
        [Column("andere_notas")]
        [StringLength(255)]
        public string andere_notas { get; set; }

        [Display(Name = "Aanvragende geneesheer")]
        [Column("aanvragende_geneesheer")]
        [StringLength(255)]
        public string aanvragende_geneesheer { get; set; }

        [Display(Name = "Patient wordt behandeld voor")]
        [Column("patient_wordt_behandeld_voor")]
        [StringLength(255)]
        public string patient_wordt_behandeld_voor { get; set; }

        [Display(Name = "Bevindingen en advies")]
        [Column("bevindingen_en_advies")]
        [StringLength(255)]
        public string bevindingen_en_advies { get; set; }

        [Display(Name = "Relevante klinische inlichtingen")]
        [Column("relevante_klinische_inlichtingen")]
        [StringLength(255)]
        public string relevante_klinische_inlichtingen { get; set; }

        [Display(Name = "Diagnostische vraagstelling")]
        [Column("diagnostische_vraagstelling")]
        [StringLength(255)]
        public string diagnostische_vraagstelling { get; set; }

        [Display(Name = "Te voet", GroupName = "Transportwijze")]
        [Column("transport_te_voet")]
        public bool? transport_te_voet { get; set; }

        [Display(Name = "Rolstoel", GroupName = "Transportwijze")]
        [Column("transport_rolstoel")]
        public bool? transport_rolstoel { get; set; }

        [Display(Name = "Bed", GroupName = "Transportwijze")]
        [Column("transport_bed")]
        public bool? transport_bed { get; set; }

        [Display(Name = "Is zwanger", GroupName = "Relevante bijkomende inlichtingen")]
        public bool? zwanger { get; set; }

        [Display(Name = "Is diabeet", GroupName = "Relevante bijkomende inlichtingen")]
        public bool? diabeet { get; set; }

        [Display(Name = "Heeft implantaat", GroupName = "Relevante bijkomende inlichtingen")]
        public bool? implantaat { get; set; }

        [Display(Name = "Heeft nierinsufficientie", GroupName = "Relevante bijkomende inlichtingen")]
        public bool? nierinsufficientie { get; set; }

        [Display(Name = "Heeft allergie", GroupName = "Relevante bijkomende inlichtingen")]
        [StringLength(255)]
        public string allergie { get; set; }

        [Display(Name = "Andere", GroupName = "Relevante bijkomende inlichtingen")]
        [StringLength(255)]
        public string andere { get; set; }
    }

}