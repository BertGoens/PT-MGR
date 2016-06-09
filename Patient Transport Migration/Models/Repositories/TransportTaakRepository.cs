using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.Repositories {
    public class TransportTaakRepository : IDisposable {
        private Context _context;

        public TransportTaakRepository(Context context) {
            _context = context;
        }

        public IEnumerable<TransportTaak> GetWachtendeTaken() {
            return _context.tblTransportTaken.Where(t =>
            t.TransportWerknemer == null &&
            t.DatumCompleet == null);
        }

        /// <summary>
        /// Krijg alle taken in de Queue voor een medewerker
        /// </summary>
        /// <param name="WerknemerId"></param>
        /// <returns></returns>
        public IEnumerable<TransportTaak> GetWerknemerTakenQueue(string WerknemerId) {
            return _context.tblTransportTaken.Where(t =>
            t.TransportWerknemerId == WerknemerId &&
            t.DatumCompleet == null);
        }

        public IEnumerable<TransportTaak> GetWerknemerTakenQueueOrdered(string WerknemerId) {
            return _context.tblTransportTaken.Where(t =>
            t.TransportWerknemerId == WerknemerId &&
            t.DatumCompleet == null)
            .OrderBy(t => t.TaakWachtrijNummer);
        }

        public TransportTaak GetTransportTaakByWerknemerByNumber(string WerknemerId, int TaakId) {
            return _context.tblTransportTaken.First(t =>
                        t.Id == TaakId &&
                        t.TransportWerknemerId == WerknemerId);
        }

        public IEnumerable<TransportTaak> GetTakenInQueueForMedewerkenNaOrderByTaakNummer(string medewerker, int taakNummerInQueue) {
            return _context.tblTransportTaken.Where(t =>
                     t.DatumCompleet == null &&
                     t.TransportWerknemerId == medewerker &&
                     t.TaakWachtrijNummer > taakNummerInQueue)
                     .OrderBy(t => t.TaakWachtrijNummer);
        }

        public IEnumerable<TransportTaak> GetTransportTakenForPatientOrderByDatum(string patientVisitId) {
            return _context.tblTransportTaken
                    .Where(a => a.Aanvraag.PatientVisit.Equals(patientVisitId))
                    .OrderBy(t => t.DatumGemaakt);
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