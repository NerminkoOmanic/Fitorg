using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FITorg.Web.Areas.AdminUser.ViewModels
{
    public class SpolIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int SpolId { get; set; }
            public string Naziv { get; set; }

        }
    }
}
