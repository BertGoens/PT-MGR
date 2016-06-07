using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Models.Core {
    public static class BehandelAanvraag {
        private const string LocatieCT = "CT";
        private const string LocatieNMR = "NMR";
        private const string LocatieRX = "RX";
        private const string LocatieEchografie = "Echografie";

        /// <summary>
        /// Maakt een transporttaak op basis van de aanvraag
        /// </summary>
        /// <param name="aanvraag">De nieuwe aanvraag</param>
        /// <param name="Kamer">De geselecteerde kamer</param>
        public static void NieuweAanvraag(Aanvraag aanvraag, Locatie Kamer = null) {
            var db = new MSSQLContext();
            var transportTaak = new TransportTaak();
            transportTaak.Aanvraag = aanvraag;
            transportTaak.DatumGemaakt = DateTime.Now;
            transportTaak.LocatieStart = aanvraag.Patient.Locatie;

            // Ga aanvraagtype van de aanvraag af en kijk naar eerste transport
            if (ZetEindLocatie(db, transportTaak, Kamer)) {
                // Opslaan
                db.tblTransportTaken.Add(transportTaak);
                db.SaveChanges();
            } else {
                db.tblExceptionLogger.Add(new ExceptionLogger() 
                    { ExceptionMessage = "Kon geen EindLocatie vinden bij creatie van Aanvraag " + aanvraag.Id }
                );
            };
        }

        private static bool ZetEindLocatie(MSSQLContext db, TransportTaak transportTaak, Locatie Kamer = null) {
            var aanvraag = transportTaak.Aanvraag;
            var AanvrType = transportTaak.Aanvraag.AanvraagType;

            if (!AanvrType.Include_Patient) {
                transportTaak.LocatieEind = Kamer;
                return true;
            } else {
                if (!AanvrType.Include_AanDokter && !AanvrType.Include_Radiologie) {
                    transportTaak.LocatieEind = Kamer;
                    return true;
                }
            }

            if (AanvrType.Include_AanDokter) {
                transportTaak.LocatieEind = aanvraag.AanDokter.Locatie;
                return true;
            }
            if (AanvrType.Include_Radiologie) {
                if (aanvraag.CT) {
                    transportTaak.LocatieEind = db.tblLocaties.First(l => l.Kamer == LocatieCT);
                    return true;
                } else if (aanvraag.NMR) {
                    transportTaak.LocatieEind = db.tblLocaties.First(l => l.Kamer == LocatieCT);
                    return true;
                } else if (aanvraag.RX) {
                    transportTaak.LocatieEind = db.tblLocaties.First(l => l.Kamer == LocatieCT);
                    return true;
                } else if (aanvraag.Echografie) {
                    transportTaak.LocatieEind = db.tblLocaties.First(l => l.Kamer == LocatieCT);
                    return true;
                }
            }
            return false;
        }

        public static void OntslagAanvraag(Aanvraag aanvraag, TransportActie transportActie) {
            var db = new MSSQLContext();
            var transportTaak = new TransportTaak();
            transportTaak.Aanvraag = aanvraag;
            transportTaak.DatumGemaakt = DateTime.Now;

            // Sinds de patient ontslagen wordt moet hij terugkeren naar zijn kamer
            transportTaak.LocatieEind = aanvraag.Patient.Locatie;

            bool nieuweActie = false;
            switch (transportActie) {
                // Zoek kamer van ontslag
                case TransportActie.DokterOntslagen:
                    nieuweActie = true;
                    transportTaak.LocatieStart = aanvraag.AanDokter.Locatie;
                    break;
                case TransportActie.CT_Ontslagen:
                    transportTaak.LocatieStart = db.tblLocaties.First(l => l.Kamer == LocatieCT);
                    break;
                case TransportActie.NMR_Ontslagen:
                    transportTaak.LocatieStart = db.tblLocaties.First(l => l.Kamer == LocatieCT);
                    break;
                case TransportActie.RX_Ontslagen:
                    transportTaak.LocatieStart = db.tblLocaties.First(l => l.Kamer == LocatieCT);
                    break;
                case TransportActie.Echografie_Ontslagen:
                    transportTaak.LocatieStart = db.tblLocaties.First(l => l.Kamer == LocatieCT);
                    break;
                case TransportActie.TransportVolbracht:
                    // Wacht op ontslag
                    break;
                default:
                    break;
            }

            if (!nieuweActie) {
                // Geen nieuwe actie = aanvraag voltooid
                aanvraag.DatumCompleet = DateTime.Now;
                db.Entry(aanvraag).State = EntityState.Modified;
                db.SaveChanges();
            } else {
                // Transporttaak maken
                db.tblTransportTaken.Add(transportTaak);
                db.SaveChanges();
            }
        }
    }
}