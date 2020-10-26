using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FITorg.Data.EntityModels
{
    public class AppUser : IdentityUser<int>
    {
        //AppUser je osnovni korisnik aplikacije
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }

        [ForeignKey("SpolId")]
        public int SpolId { get; set; }
        public virtual Spol Spol { get; set; }

        [ForeignKey("GradId")]
        public int GradId { get; set; }
        public virtual Grad Grad { get; set; }

        [ForeignKey("DrzavaId")]
        public int DrzavaId { get; set; }
        public virtual Drzava Drzava { get; set; }


    }
}
