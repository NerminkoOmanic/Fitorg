using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FITorg.Web.Areas.AdminUser.ViewModels
{
    public class GradoviDodajVM
    {
        public int GradId { get; set; }
        public string Naziv { get; set; }
        public int DrzavaId { get; set; }
        public List<SelectListItem> DrzaveItems { get; set; }
    }
}

