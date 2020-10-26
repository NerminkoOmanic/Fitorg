using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FITorg.Data.EntityModels
{
    public class Drzava
    {
        [Key]
        public int DrzavaId { get; set; }
        public string Naziv { get; set; }
        public List<Grad> GradoviList { get; set; }

        public Drzava()
        {
            GradoviList=new List<Grad>();
        }
    }
}
