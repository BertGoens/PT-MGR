using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Model;
using Patient_Transport_Migration.Models.POCO;
using Patient_Transport_Migration.Models.Repositories;

namespace Patient_Transport_Migration.Models.Core {
    /// <summary>
    /// Logica omtrent het maken & verwerken van transport-taken
    /// </summary>
    public static class BehandelTaak {
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
                if (taak.TransportGestart) {
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

            if (aanvraag.AanvraagType.Include_Patient) {
                transportTaak.LocatieStart = aanvraag.Patient.Locatie;
            }

            // Ga aanvraagtype van de aanvraag af en kijk naar eerste transport
            if (ZetEindLocatie(context, transportTaak, Kamer)) {
                // Opslaan
                context.tblTransportTaken.Add(transportTaak);
                context.SaveChanges();
            } else {
                context.tblExceptionLogger.Add(new ExceptionLogger() { ExceptionMessage = "Kon geen EindLocatie vinden bij creatie van Aanvraag " + aanvraag.Id }
                );
            };
        }

        // Gebruikt door NieuweAanvraag
        private static bool ZetEindLocatie(Context context, TransportTaak transportTaak, Locatie Kamer = null) {
            var aanvraag = transportTaak.Aanvraag;
            var AanvrType = transportTaak.Aanvraag.AanvraagType;

            if (!AanvrType.Include_Patient) {
                transportTaak.LocatieEind = Kamer;
                return true;
            }

            if (AanvrType.Include_AanDokter) {
                transportTaak.DokterEind = aanvraag.AanDokter;
                transportTaak.LocatieEind = aanvraag.AanDokter.Locatie;
                return true;
            }

            return false;
        }

        public static bool StartTransport(Context context, TransportTaak transportTaak) {
            transportTaak.TransportGestart = true;
            context.SaveChanges();
            return true;
        }

        public static bool AnnuleerTransport(Context context, TransportTaak transportTaak) {
            transportTaak.TransportGestart = false;
            context.SaveChanges();
            return true;
        }

        public static bool EindeTransport(Context context, TransportTaak taak) {
            // Huidige taak afwerkstatus = afgewerkt
            taak.DatumCompleet = DateTime.Now;
            if (taak.Aanvraag.AanvraagType.Include_Patient) {
                taak.Aanvraag.PatientBij = taak.DokterEind;
            }

            // Pas Queue aan

            var TakenNaDeze = new TransportTaakRepository(context).GetTakenInQueueForMedewerkenNaOrderByTaakNummer(
                taak.TransportWerknemer.Gebruikersnaam, (int)taak.TaakWachtrijNummer);
            for (int i = 0; i < TakenNaDeze.Count(); i++) {
                var taakVeranderQueueNr = TakenNaDeze.ElementAt(i);
                taakVeranderQueueNr.TaakWachtrijNummer -= 1;
                context.Entry(taakVeranderQueueNr).State = EntityState.Modified;
            }
            context.SaveChanges();
            return true;
        }

        public static void OntslagAanvraag(Context context, Aanvraag aanvraag, Dokter Dokter) {
            aanvraag.DatumCompleet = DateTime.Now;
            aanvraag.PatientBij = null;

            var transportTaak = new TransportTaak();
            transportTaak.Aanvraag = aanvraag;
            transportTaak.Aanvraag = aanvraag;
            transportTaak.DatumGemaakt = DateTime.Now;
            transportTaak.LocatieStart = Dokter.Locatie;
            // Sinds de patient ontslagen wordt moet hij terugkeren naar zijn kamer
            transportTaak.LocatieEind = aanvraag.Patient.Locatie;

            //Opslaan
            context.Entry(aanvraag).State = EntityState.Modified;
            context.tblTransportTaken.Add(transportTaak);
            context.SaveChanges();
        }
    }

}
