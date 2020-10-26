using System.Collections.Generic;

namespace FITorg.Web.Areas.TrUser.ViewModels
{
    public class VjezbeIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int VjezbaId {get; set; }
            public string Naziv {get; set; }
            public string videoUrl {get; set; }
        }
    }
}
