using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FITorg.Data.EntityModels
{
    public class Trener 
    {
        public int TrenerId { get; set; }

        [ForeignKey("AppUserId")]
        public int AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }

        public List<Sportas> SportasiList {get; set;}

        public Trener()
        {
            SportasiList=new List<Sportas>();
        }

    }
}
