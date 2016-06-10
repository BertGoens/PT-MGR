using System.Collections.Generic;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.VM.DokterPraktijk {
    public class PatientenVoorDokterVM {
        public PatientenVoorDokterVM(IEnumerable<TransportTaak> DokterTaken, Dokter Dok) {
            this.TransportTakenVoorDokter = DokterTaken;
            this.Dokter = Dok;
        }

        public IEnumerable<TransportTaak> TransportTakenVoorDokter { get; private set; }

        public Dokter Dokter { get; private set; }
    }
}