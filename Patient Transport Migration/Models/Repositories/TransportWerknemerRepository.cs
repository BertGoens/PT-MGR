﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.Repositories {
    public class TransportWerknemerRepository : IDisposable {
        private Context _context;

        public TransportWerknemerRepository(Context context) {
            _context = context;
        }

        public IEnumerable<TransportWerknemer> GetAll() {
            return _context.tblTransportWerknemers.ToList();
        }

        public TransportWerknemer GetWerknemerByGebruikersnaam(string Gebruikersnaam) {
            return _context.tblTransportWerknemers.First(w => w.Gebruikersnaam == Gebruikersnaam);
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