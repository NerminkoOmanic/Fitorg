using System.Collections.Generic;

namespace FITorg.Web.Areas.TrUser.ViewModels
{
    public class SportasiIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int SportasId {get; set; }
            public string ImePrezime {get; set; }
            public string Email {get; set; }
            public string Phone {get; set; }
            public string NazivGrada { get; set; }
        }
    }
}
