using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FITorg.Data.EntityModels
{
    public class Sportas 
    {
        public int SportasId { get; set; }


        [ForeignKey("AppUserId")]
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }


        [ForeignKey("TrenerId")]
        public int TrenerId { get; set; }
        public virtual Trener Trener { get; set; }


        public string Visina { get; set; }
        public float Tezina { get; set; }
        public string UdioMasti { get; set; }
        public List<Napredak> NapredakList{get; set;}

        public Sportas()
        {
            NapredakList = new List<Napredak>();
        }
    }
}
