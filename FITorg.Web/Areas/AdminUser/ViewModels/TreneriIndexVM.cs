using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FITorg.Web.Areas.AdminUser.ViewModels
{
    public class TreneriIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int TrenerId {get; set; }
            public string ImePrezime {get; set; }
            public string email {get; set; }
            public string NazivGrada { get; set; }
            public string NazivDrzave { get; set; }
            public string Spol { get; set; }


        }
    }
}
