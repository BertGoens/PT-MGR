using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.POCO {
    public class Transportwijze {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Omschrijving { get; set; }
    }
}