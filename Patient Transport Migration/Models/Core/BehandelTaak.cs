using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Model;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Models.Core {
    /// <summary>
    /// Logica omtrent het maken & verwerken van transport-taken
    /// </summary>
    public static class BehandelAanvraag {
        /* Hardcoded IDs van verplichte 'Dokters' */
        private const string ID_CT = "CT";
        private const string ID_NMR = "NMR";
        private const string ID_RX = "RX";
        private const string ID_Echografie = "Echografie";

        /// <summary>
        /// De (berekende) status van de taak.
        /// </summary>
        public static TransportTaakStatus GetTransportTaakStatus(TransportTaak taak) {
            if (taak.DatumCompleet != null) {
                return TransportTaakStatus.Voltooid;
            }

            if (taak.TaakWachtrijNummer != null) {
                if (taak.TaakWachtrijNummer == 0) {
                    return TransportTaakStatus.WerknemerToegewezen_HuidigeTaak;
                }

                return TransportTaakStatus.WerknemerToegewezen_Wachtend;
            }

            return TransportTaakStatus.NietToegewezen_Wachtend;
        }

        /// <summary>
        /// Maakt een transporttaak op basis van de aanvraag
        /// </summary>
        /// <param name="aanvraag">De nieuwe aanvraag</param>
        /// <param name="Kamer">De geselecteerde kamer als het geen dokter is</param>
        public static void NieuweAanvraag(Context context, Aanvraag aanvraag, Locatie Kamer = null) {
            var transportTaak = new TransportTaak();
            transportTaak.Aanvraag = aanvraag;
            transportTaak.DatumGemaakt = DateTime.Now;
            transportTaak.LocatieStart = aanvraag.Patient.Locatie;

            // Ga aanvraagtype van de aanvraag af en kijk naar eerste transport
            if (ZetEindLocatie(context, transportTaak, Kamer)) {
                // Opslaan
                context.tblTransportTaken.Add(transportTaak);
                context.SaveChanges();
            } else {
                context.tblExceptionLogger.Add(new ExceptionLogger() 
                    { ExceptionMessage = "Kon geen EindLocatie vinden bij creatie van Aanvraag " + aanvraag.Id }
                );
            };
        }

        private static bool ZetEindLocatie(Context context, TransportTaak transportTaak, Locatie Kamer = null) {
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
                    transportTaak.LocatieEind = context.tblDokters.First(l => l.Id == ID_CT).Locatie;
                    return true;
                } else if (aanvraag.NMR) {
                    transportTaak.LocatieEind = context.tblDokters.First(l => l.Id == ID_NMR).Locatie;
                    return true;
                } else if (aanvraag.RX) {
                    transportTaak.LocatieEind = context.tblDokters.First(l => l.Id == ID_RX).Locatie;
                    return true;
                } else if (aanvraag.Echografie) {
                    transportTaak.LocatieEind = context.tblDokters.First(l => l.Id == ID_Echografie).Locatie;
                    return true;
                }
            }
            return false;
        }

        public static void OntslagAanvraag(Context context, Aanvraag aanvraag, TransportActie transportActie) {
            var transportTaak = new TransportTaak();
            transportTaak.Aanvraag = aanvraag;
            transportTaak.DatumGemaakt = DateTime.Now;

            // Sinds de patient ontslagen wordt moet hij terugkeren naar zijn kamer
            transportTaak.LocatieEind = aanvraag.Patient.Locatie;

            bool nieuweActie = false;
            switch (transportActie) {
                // Zoek kamer van ontslag
                case TransportActie.Dokter_Ontslagen:
                    nieuweActie = true;
                    transportTaak.LocatieStart = aanvraag.AanDokter.Locatie;
                    break;
                case TransportActie.CT_Ontslagen:
                    nieuweActie = true;
                    transportTaak.LocatieStart = context.tblDokters.First(l => l.Id == ID_CT).Locatie;
                    break;
                case TransportActie.NMR_Ontslagen:
                    nieuweActie = true;
                    transportTaak.LocatieStart = context.tblDokters.First(l => l.Id == ID_NMR).Locatie;
                    break;
                case TransportActie.RX_Ontslagen:
                    nieuweActie = true;
                    transportTaak.LocatieStart = context.tblDokters.First(l => l.Id == ID_RX).Locatie;
                    break;
                case TransportActie.Echografie_Ontslagen:
                    nieuweActie = true;
                    transportTaak.LocatieStart = context.tblDokters.First(l => l.Id == ID_Echografie).Locatie;
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
                context.Entry(aanvraag).State = EntityState.Modified;
                context.SaveChanges();
            } else {
                // Nieuwe TransportTaak maken
                context.tblTransportTaken.Add(transportTaak);
                context.SaveChanges();
            }
        }
    }
}