using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM {
    public class VerplegingOverzichtVM {
        public List<TransportTask> PatientInOrde { get; set; }

        public List<TransportTask> PatientenToDo { get; set; }
    }
}