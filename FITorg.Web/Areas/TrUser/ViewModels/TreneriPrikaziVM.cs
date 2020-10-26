using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FITorg.Web.Areas.TrUser.ViewModels
{
    public class TreneriPrikaziVM
    {
        
            public int TrenerID { get; set;}
            [Required]
            public string Ime { get; set; }
            [Required]
            public string Prezime { get; set; }
            [Required]
            public string Email { get; set; }
            public string Password { get; set; }
            [Required]
            public string Mob {get;set;}
            public DateTime DatumRodjenja { get; set; }
            public string GradNaziv { get; set; }
            public string DrzavaNaziv { get; set; }
            public string SpolNaziv { get; set; }
            
        
    }
}
