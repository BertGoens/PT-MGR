using System;
using System.Data.Entity.Migrations;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Migrations {
    public class Configuration : DbMigrationsConfiguration<Models.MSSQLContext> {

        public Configuration() {
            AutomaticMigrationDataLossAllowed = true;
            // TODO Uitzetten AutomaticMigrationsEnabled in production anders gg data: https://coding.abel.nu/2012/03/ef-migrations-command-reference/
            //AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MSSQLContext context) {
            /*
            // Dokter
            var docBert = new Dokter() { Id = "doc001", Naam = "Bert Goens", IsConsultDokter = true, IsConsultVerwachtend = true };
            var docTijl = new Dokter() { Id = "doc002", Naam = "Tijl Goens", IsConsultDokter = true, IsConsultVerwachtend = false };
            var docBob = new Dokter() { Id = "doc003", Naam = "Bob Caesar", IsConsultDokter = false, IsConsultVerwachtend = true };
            context.tblDokters.AddOrUpdate(d => d.Id, docBert, docTijl, docBob);

            // Patient
            var patMichelle = new Patient() {
                Id = "pat001", PatientVisit = "patvis001", Voornaam = "Michelle", Achternaam = "Obama", Geslacht = Geslacht.Vrouw,
                Geboortedatum = new DateTime(1964, 1, 17), BehandelendeDokterId = "doc001", Afdeling = "1", Kamer = "100", BedNr = "02" };
            var patObama = new Patient() {
                Id = "pat002", PatientVisit = "patvis002", Voornaam = "Barack", Achternaam = "Obama", Geslacht = Geslacht.Man,
                Geboortedatum = new DateTime(1961, 8, 4), BehandelendeDokterId = "doc001", Afdeling = "1", Kamer = "100", BedNr = "01" };

            context.tblPatienten.AddOrUpdate(p => p.Id, patMichelle, patObama );

            // Transportwijze
            var trTeVoet = new Transportwijze() { Id = 1, Omschrijving = "Te Voet" };
            var trRolstoel = new Transportwijze() { Id = 2, Omschrijving = "Rolstoel" };
            var trBed = new Transportwijze() { Id = 3, Omschrijving = "Bed" };
            context.tblTransportwijzes.AddOrUpdate(t => t.Id, trTeVoet, trRolstoel, trBed);

            // AanvraagType
            var atVervoerAanvraag = new AanvraagType() { Id = 1, Omschrijving = "Vervoer Aanvraag", Include_er_Omschrijving = true };
            var atPatientVervoerAanvraag = new AanvraagType() { Id = 2, Omschrijving = "Patient Vervoer Aanvraag", Include_er_Omschrijving = true, Include_Patient = true, Include_avr_Transportwijze = true };

            var atAanvraagVanConsult = new AanvraagType() {
                Id = 3, Omschrijving = "Aanvraag van Consult", Include_Patient = true, Include_PatientVisit = true,
                Include_avc_AanDokter = true, Include_avc_AanvragendeGeneesheer = true,
                Include_avc_AndereNotas = true, Include_avc_BevindingenEnAdvies = true, Include_avc_DatumBevindingen = true,
                Include_avc_HuidigeKlachten = true, Include_avc_PatientWordtBehandeldVoor = true, Include_avc_UwAdviesGevraagdVoor = true
            };
            var atAanvraagVoorRadiologie = new AanvraagType() {
                Id = 4, Omschrijving = "Aanvraag voor Radiologie", Include_Patient = true, Include_PatientVisit = true,
                Include_avr_Allergieen = true, Include_avr_Andere = true,
                Include_avr_AndereInlichtingen = true, Include_avr_CT = true, Include_avr_DiagnostischeVraagstelling = true,
                Include_avr_Echografie = true, Include_avr_HeeftImplantaat = true, Include_avr_HeeftNierInsufficientie = true,
                Include_avr_IsDiabeet = true, Include_avr_IsZwanger = true, Include_avr_NMR = true, Include_avr_Onbekend = true,
                Include_avr_RelevanteKlinischeInlichtingen = true, Include_avr_RX = true, Include_avr_Transportwijze = true,
                Include_avr_VoorgesteldeOnderzoeken = true
            };

            context.tblAanvraagTypes.AddOrUpdate(at => at.Id, atVervoerAanvraag, atPatientVervoerAanvraag);
            // AanvraagStatus


            // TransportWerknemer
            var wnStaIt2 = new TransportWerknemer() { Gebruikersnaam = "sta_it2", Achternaam = "Goens", Voornaam = "Bert", IsPresent = true };
            var wnPTTransport = new TransportWerknemer() { Gebruikersnaam = "PTTransport", Achternaam = "Test", Voornaam = "Transport", IsPresent = false };
            context.tblTransportWerknemers.AddOrUpdate(w => w.Gebruikersnaam, wnStaIt2, wnPTTransport);

            // Aanvraag
            var aanvrdataVervoer1 = new Aanvraag() { Id = 1, AanvraagType = atVervoerAanvraag, va_Omschrijving = "Haal Baxters" };
            var aanvrdataVervoer2 = new Aanvraag() { Id = 2, AanvraagType = atVervoerAanvraag, va_Omschrijving = "Haal Naalden" };
            var aanvrdataPatVervoerMichelle = new Aanvraag() { Id = 3, AanvraagType = atPatientVervoerAanvraag, va_Omschrijving = "Patient moet naar dokter V.Putin", Patient = patMichelle, avr_Transportwijze = trBed };
            var aanvrdataPatBrengObamaTerug = new Aanvraag() { Id = 4, AanvraagType = atPatientVervoerAanvraag, va_Omschrijving = "Patient naar kamer", Patient = patObama, avr_Transportwijze = trBed };
            context.tblAanvragen.AddOrUpdate(aa => aa.Id, aanvrdataVervoer1, aanvrdataVervoer2, aanvrdataPatVervoerMichelle, aanvrdataPatBrengObamaTerug);

            // TransportTaak
            int achtMinuten = 8 * 60;
            var transpVerplaatMichelleNaarNMR = new TransportTaak() {
                Id = 1, Aanvraag = aanvrdataPatVervoerMichelle, DatumGemaakt = DateTime.Now, GeschatteTijdNodigInSeconden = achtMinuten,
                IsPrioriteitHoog = false, LocatieStart = patMichelle.Kamer, LocatieEind = "NMR" };
            var transpHaalNaalden = new TransportTaak() {
                Id = 2, Aanvraag = aanvrdataVervoer2, DatumGemaakt = DateTime.Now, GeschatteTijdNodigInSeconden = achtMinuten,
                IsPrioriteitHoog = true, LocatieStart = "Kamer 200", LocatieEind = "Onthaal"};
            var datumCompleet = new DateTime(2015, 1, 18);
            var transpObamaVerplaatst = new TransportTaak() {
                Id = 3, Aanvraag = aanvrdataPatBrengObamaTerug, DatumGemaakt = datumCompleet, DatumCompleet = datumCompleet, LocatieStart = "NMR",
                LocatieEind = patObama.Kamer, GeschatteTijdNodigInSeconden = achtMinuten, TijdNodigInSeconden = achtMinuten, TransportWerknemer = wnPTTransport, IsPrioriteitHoog = false
            };

            context.tblTransportTaken.AddOrUpdate(t => t.Id, transpVerplaatMichelleNaarNMR, transpHaalNaalden, transpObamaVerplaatst);               
            */
            base.Seed(context);
            
        }

    }
}