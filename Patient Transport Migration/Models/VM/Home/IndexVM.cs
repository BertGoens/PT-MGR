using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patient_Transport_Migration.Models.VM.Home {
    public class IndexVM {
        public IndexVM(bool isAdmin) {
            this.IsAdmin = isAdmin;
        }

        public bool IsAdmin { get; private set; }
    }
}