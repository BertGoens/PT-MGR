using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Model;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.Repositories {
    public class LocatieRepository : IDisposable {
        private Context _context;

        public LocatieRepository(Context context) {
            _context = context;
        }

        public IEnumerable<Locatie> GetKamersVanAfdeling(string AfdelingCode) {
            return _context.tblLocaties.Where(l => l.Afdeling == AfdelingCode);
        }

        public IEnumerable<Afdeling> GetUniekeAfdelingen() {
            return _context.tblLocaties.Select(l =>
                    new Afdeling() { Omschrijving = l.Omschrijving, Code = l.Afdeling })
                    .Distinct()
                    .ToList();
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