using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.Repositories;
using Patient_Transport_Migration.Models.Util;

namespace Patient_Transport_Migration.Models.VM.DispatchOverzicht {
    public class WachtendeTransportTakenVM {
        public WachtendeTransportTakenVM(int? page) {
            var taken = new TransportTaakRepository().GetWachtendeTaken();
            Pager = new Pager(taken.Count(), page);
            _TransportTaken = taken.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();
            _TransportWerknemers = new TransportWerknemerRepository().GetAll();
        }

        private IEnumerable<TransportTaak> _TransportTaken { get; set; }
        public IEnumerable<TransportTaak> TransportTaken { get { return _TransportTaken; } }

        private IEnumerable<TransportWerknemer> _TransportWerknemers { get; set; }
        public IEnumerable<SelectListItem> TransportWerknemerItems {
            get {
                return _TransportWerknemers.Select(tw => new SelectListItem {
                    Text = tw.Naam(),
                    Value = tw.Gebruikersnaam
                });
            }
        }

        public Pager Pager { get; private set; }
    }
}