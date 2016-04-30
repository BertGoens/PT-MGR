using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    [MetadataType(typeof(RequestStatusMetaData))]
    public partial class RequestStatus {

        public int Fk_Request_Type { get; set; }
        public virtual RequestType Request_Type { get; set; }

        public int Pk_Id { get; set; }
        public string Description { get; set; }
        public int Next { get; set; }
    }

    [Table("Request_Status")]
    public class RequestStatusMetaData {

        public int Fk_Request_Type { get; set; }

        public virtual RequestType Request_Type { get; set; }

        [Key]
        public int Pk_Id { get; set; }

        [Display(Name = "Beschrijving")]
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; }

        [Display(Name = "Volgende Stap")]
        [Column("next")]
        public int Next { get; set; }
    }
}