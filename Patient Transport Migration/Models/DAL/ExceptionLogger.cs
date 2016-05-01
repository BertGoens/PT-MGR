using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Transport_Migration.Models.DAL {
    public class ExceptionLogger
    {
        public ExceptionLogger() {
            LogTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(1000)]
        public string ExceptionMessage { get; set; }
        [MaxLength(255)]
        public string ControllerName { get; set; }
        [MaxLength(8000)]
        public string ExceptionStackTrace { get; set; }
        public DateTime LogTime { get; set; }

    }
}
