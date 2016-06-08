using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.Repositories {
    public class DokterRepository {
        private DokterContext _context;

        public DokterRepository(DokterContext context) {
            _context = context;
        }

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