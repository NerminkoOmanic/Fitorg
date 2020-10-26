using System.Collections.Generic;

namespace FITorg.Web.Areas.TrUser.ViewModels
{
    public class VjezbeDodajVM
    {
        public int VjezbaId {get; set; }
        public string Naziv {get; set; }
        public string videoUrl {get; set; }
        public int AppUserId { get; set; }
    }
}
