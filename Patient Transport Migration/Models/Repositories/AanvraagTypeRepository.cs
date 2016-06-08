using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.Repositories {
    public class AanvraagTypeRepository : IDisposable {
        private Context _context;

        public AanvraagTypeRepository(Context context) {
            _context = context;
        }

        public IEnumerable<AanvraagType> GetAanvraagTypesWithPatient() {
           return _context.tblAanvraagTypes.Where(a => a.Include_Patient).ToList();
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