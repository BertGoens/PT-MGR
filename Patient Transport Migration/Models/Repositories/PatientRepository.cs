using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.Repositories {
    public class PatientRepository : IDisposable {
        private Context _context;
        public PatientRepository(Context context) {
            _context = context;
        }

        public Patient GetPatientByVisitId(string visitId) {
            return _context.tblPatienten.First(p => p.PatientVisit.Equals(visitId));
        }

        public IEnumerable<Patient> GetPatientenOrderByAchternaam() {
            return _context.tblPatienten.OrderBy(p => p.Achternaam);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}