using System.Collections.Generic;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.VM {
    public class PatientInfoVM {

        public int PatientSelected { get; set; }
        /// <summary>
        /// Used by PatientInfo
        /// </summary>
        public SelectList Patients { get; set; }

        /// <summary>
        /// Used by _PatientDetails
        /// </summary>
        public Patient PatientDetails { get; set; }

        /// <summary>
        /// Used by _PatientDetails
        /// </summary>
        public List<TransportTask> PatientRequests { get; set; }

        public int RequestTypeSelected { get; set; }
        /// <summary>
        /// Used by _RequestTypes
        /// </summary>
        public SelectList RequestTypes { get; set; }

        /// <summary>
        /// Used by _RequestDetails
        /// </summary>
        public TransportTask RequestDetails { get; set; }
    }
}