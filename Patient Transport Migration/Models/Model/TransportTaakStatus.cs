using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patient_Transport_Migration.Models.Model {
    /// <summary>
    /// Transport Taak Status
    /// </summary>
    public enum TransportTaakStatus {
        NietToegewezen_Wachtend,
        WerknemerToegewezen_Wachtend,
        WerknemerToegewezen_HuidigeTaak,
        Voltooid
    }
}