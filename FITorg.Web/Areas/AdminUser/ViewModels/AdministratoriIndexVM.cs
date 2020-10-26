using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FITorg.Web.Areas.AdminUser.ViewModels
{
    public class AdministratoriIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int AdminId {get; set; }
            public string ImePrezime {get; set; }
            public string Email {get; set; }
            public string NazivGrada { get; set; }
            public string NazivDrzave { get; set; }
            public string IzjavaOZastitiPodatak { get; set; }
            public string BrojMob {get; set; }


        }
    }
}
