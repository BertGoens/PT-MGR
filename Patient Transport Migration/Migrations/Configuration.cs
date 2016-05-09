using System;
using System.Data.Entity.Migrations;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Migrations {
    public class Configuration : DbMigrationsConfiguration<Models.MSSQLContext> {

        public Configuration() {
            AutomaticMigrationDataLossAllowed = true;
            // TODO Uitzetten AutomaticMigrationsEnabled in production anders gg data: https://coding.abel.nu/2012/03/ef-migrations-command-reference/
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MSSQLContext context) {
            // Dokter
            context.tblDokters.AddOrUpdate(d => d.Id,
                new Dokter() { Id = "doc001", Naam = "Bert Goens", IsConsultDokter = true, IsConsultVerwachtend = true },
                new Dokter() { Id = "doc002", Naam = "Tijl Goens", IsConsultDokter = true, IsConsultVerwachtend = false },
                new Dokter() { Id = "doc003", Naam = "Bob Caesar", IsConsultDokter = false, IsConsultVerwachtend = true }
                );

            // Patient
            context.tblPatienten.AddOrUpdate(p => p.Id,
                new Patient() {
                    Id = "pat001", PatientVisit = "patvis001", Voornaam = "Michelle", Achternaam = "Obama", Geslacht = Geslacht.Vrouw,
                    Geboortedatum = new DateTime(1964, 1, 17), BehandelendeDokterId = "doc001", Afdeling = "1", Kamer = "100", BedNr = "02" },
                new Patient() {
                    Id = "pat002", PatientVisit = "patvis002", Voornaam = "Barack", Achternaam = "Obama", Geslacht = Geslacht.Man,
                    Geboortedatum = new DateTime(1961, 8, 4), BehandelendeDokterId = "doc001", Afdeling = "1", Kamer = "100", BedNr = "01"}
                );

            // Transportwijze
            context.tblTransportwijzes.AddOrUpdate(t => t.Id,
                new Transportwijze() { Id = 1, Omschrijving = "Te Voet"},
                new Transportwijze() { Id = 2, Omschrijving = "Rolstoel"},
                new Transportwijze() { Id = 3, Omschrijving = "Bed"}
                );

            // AanvraagType
            context.tblAanvraagTypes.AddOrUpdate(at => at.Id,
                new AanvraagType() { Id = 1, Omschrijving = "Vervoer Aanvraag", Include_er_Omschrijving = true },
                new AanvraagType() { Id = 2, Omschrijving = "Aanvraag van Consult", Include_Patient = true, Include_PatientVisit = true,
                    Include_avc_AanDokter = true, Include_avc_AanvragendeGeneesheer = true,
                    Include_avc_AndereNotas = true, Include_avc_BevindingenEnAdvies = true, Include_avc_DatumBevindingen = true,
                    Include_avc_HuidigeKlachten = true, Include_avc_PatientWordtBehandeldVoor = true, Include_avc_UwAdviesGevraagdVoor = true },
                new AanvraagType() { Id = 3, Omschrijving = "Aanvraag voor Radiologie", Include_Patient = true, Include_PatientVisit = true,
                    Include_avr_Allergieen = true, Include_avr_Andere = true,
                    Include_avr_AndereInlichtingen = true, Include_avr_CT = true, Include_avr_DiagnostischeVraagstelling = true,
                    Include_avr_Echografie = true, Include_avr_HeeftImplantaat = true, Include_avr_HeeftNierInsufficientie = true,
                    Include_avr_IsDiabeet = true, Include_avr_IsZwanger = true, Include_avr_NMR = true, Include_avr_Onbekend = true,
                    Include_avr_RelevanteKlinischeInlichtingen = true, Include_avr_RX = true, Include_avr_Transportwijze = true,
                    Include_avr_VoorgesteldeOnderzoeken = true },
                new AanvraagType() { Id = 4, Omschrijving = "Patient Vervoer Aanvraag",
                    Include_er_Omschrijving = true, Include_Patient = true, Include_avr_Transportwijze = true }
                );

            // AanvraagStatus
            

            // TransportWerknemer
            context.tblTransportWerknemers.AddOrUpdate(w => w.Gebruikersnaam,
                new TransportWerknemer() { Gebruikersnaam = "sta_it2", Achternaam = "Goens", Voornaam = "Bert", IsPresent = true },
                new TransportWerknemer() { Gebruikersnaam = "PTTransport", Achternaam = "Test", Voornaam = "Transport", IsPresent = false }
                );

            // Aanvraag
            context.tblAanvragen.AddOrUpdate(aa => aa.Id,
                new Aanvraag() { Id = 1, AanvraagTypeId = 1, va_Omschrijving = "Haal Baxters" },
                new Aanvraag() { Id = 2, AanvraagTypeId = 1, va_Omschrijving = "Haal Naalden" },
                new Aanvraag() { Id = 3, AanvraagTypeId = 2, PatientId = "pat001", avc_AanDokterId = "doc001"}
                );

            // TransportTaak
            int achtMinuten = 8 * 60;
            context.tblTransportTaken.AddOrUpdate(t => t.Id,
                new TransportTaak() {
                    Id = 1, AanvraagId = 1, DatumGemaakt = DateTime.Now, GeschatteTijdNodigInSeconden = achtMinuten,
                    IsPrioriteitHoog = false, LocatieStart = "101", LocatieEind = "300"
                },
                new TransportTaak() { Id = 4, AanvraagId = 2, DatumGemaakt = DateTime.Now, GeschatteTijdNodigInSeconden = achtMinuten,
                    IsPrioriteitHoog = true, LocatieStart = "Kamer 200", LocatieEind = "Onthaal"}
                );

            //TransportWerknemerTaak

            base.Seed(context);
        }

    }
}