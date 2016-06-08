using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Migrations {
    public class Configuration : DbMigrationsConfiguration<Models.DAL.Context> {

        public Configuration() {
             // #Standaard uit tenzij je een database update wilt doen met nieuwe seed data
             // #Bv een mock repository maken om unit tests te maken ofzo.
              AutomaticMigrationDataLossAllowed = true;
              AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Context context) {
            /*
            // Vereiste locaties
            var LocCT = new Locatie { Afdeling = "0000", Kamer = "CT", Omschrijving = "Radiologie" };
            var LocNMR = new Locatie { Afdeling = "0000", Kamer = "NMR", Omschrijving = "Radiologie" };
            var LocRX = new Locatie { Afdeling = "0000", Kamer = "RX", Omschrijving = "Radiologie" };
            var LocEchografie = new Locatie { Afdeling = "0000", Kamer = "Echografie", Omschrijving = "Radiologie" };

            var LocChirurgieA = new Locatie { Afdeling = "2101", Kamer = "216", Omschrijving = "Chirurgie A" };
            var LocChirurgieB = new Locatie { Afdeling = "2102", Kamer = "301", Omschrijving = "Chirurgie B" };

            var Locaties = new List<Locatie> {
                LocCT, LocNMR, LocRX, LocEchografie,
                new Locatie { Afdeling = "1500", Kamer = "501", Omschrijving = "Pediatrie" },
                new Locatie { Afdeling = "1500", Kamer = "516", Omschrijving = "Pediatrie" },

                new Locatie { Afdeling = "2101", Kamer = "201", Omschrijving = "Chirurgie A" },
                LocChirurgieA,

                LocChirurgieB,
                new Locatie { Afdeling = "2102", Kamer = "319", Omschrijving = "Chirurgie B" }
             };
            Locaties.ForEach(l => context.tblLocaties.AddOrUpdate(p => p.Afdeling, l));

            var Dokters = new List<Dokter> {
                // Vereiste dokters
                new Dokter { Id = "CT", Naam = "CT", IsConsultVerwachtend = true, GebruikersNaam = "CT", Locatie = LocCT },
                new Dokter { Id = "NMR", Naam = "NMR", IsConsultVerwachtend = true, GebruikersNaam = "NMR", Locatie = LocNMR },
                new Dokter { Id = "RX", Naam = "RX", IsConsultVerwachtend = true, GebruikersNaam = "CT", Locatie = LocRX },
                new Dokter { Id = "Echografie", Naam = "Echografie", IsConsultVerwachtend = true, GebruikersNaam = "Echografie", Locatie = LocEchografie },
                new Dokter { Id = "3ab43", Naam = "Dr. Jos", IsConsultVerwachtend = true, GebruikersNaam = "sta_it" },
                new Dokter { Id = "x9s33", Naam = "Dr. Willy", IsConsultVerwachtend = false, GebruikersNaam = "sta_it" }
            };
            Dokters.ForEach(d => context.tblDokters.AddOrUpdate(s => s.Id, d));

            var date = new DateTime(1991, 2, 23);
            var Patienten = new List<Patient> {
                new Patient { PatientId = "000000", PatientVisit = "010101", Voornaam = "John", Achternaam = "Doe",
                Geboortedatum = date, Geslacht = Geslacht.Man, BedNr = "02",
                    Locatie = LocChirurgieA }
            };
            Patienten.ForEach(t => context.tblPatienten.AddOrUpdate(p => p.PatientId, t));

            var TakenTypes = new List<AanvraagType> {
                new AanvraagType { Id = 1, DatumGemaakt = DateTime.Now, Omschrijving = "Vervoer" },
                new AanvraagType { Id = 2, DatumGemaakt = DateTime.Now, Omschrijving = "Patient Vervoer", Include_Patient = true, Include_Transportwijze = true },
                new AanvraagType { Id = 3, DatumGemaakt = DateTime.Now, Omschrijving = "Aanvraag van Consult", Include_AanDokter = true, Include_Patient = true, Include_Transportwijze = true }
            };
            TakenTypes.ForEach(t => context.tblAanvraagTypes.AddOrUpdate(p => p.Id, t));

            var TranspWn = new List<TransportWerknemer> {
                new TransportWerknemer { Gebruikersnaam = "sta_it2", Achternaam = "Goens", Voornaam = "Bert", IsPresent = true },
                new TransportWerknemer { Gebruikersnaam = "pttransport", Achternaam = "Transport", Voornaam = "Test", IsPresent = false }
            };
            TranspWn.ForEach(tw => context.tblTransportWerknemers.AddOrUpdate(p => p.Gebruikersnaam, tw));

            var TranspWijze = new List<Transportwijze> {
                new Transportwijze { Id = 1, Omschrijving = "Te Voet" },
                new Transportwijze { Id = 2, Omschrijving = "Rolstoel" },
                new Transportwijze { Id = 3, Omschrijving = "Bed" }
            };
            TranspWijze.ForEach(t => context.tblTransportwijzes.AddOrUpdate(p => p.Id, t));
            */
        }
    }
}