using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.Repositories {
    public class DokterRepository : IDisposable {
        private Context _context;

        public DokterRepository(Context context) {
            _context = context;
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

        // Radiologie Dokters
        private const string Dok_CT_ID = "CT";
        private const string Dok_CT_NMR = "NMR";
        private const string Dok_CT_RX = "RX";
        private const string Dok_CT_Echografie = "Echografie";
        public IEnumerable<Dokter> GetDoktersExcludeRadiologie() {
            IEnumerable<Dokter> result = _context.tblDokters.Where(d =>
            d.Id != Dok_CT_ID &&
            d.Id != Dok_CT_NMR &&
            d.Id != Dok_CT_RX &&
            d.Id != Dok_CT_Echografie
            );
            return result;
        }

    }
}