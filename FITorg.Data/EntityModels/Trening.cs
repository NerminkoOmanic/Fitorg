using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FITorg.Data.EntityModels
{
    public class Trening 
    {
        public string TreningId { get; set; }
        
        [ForeignKey("SportasId")]
        public int SportasId { get; set; }
        public virtual Sportas Sportas { get; set; }

        public DateTime Datum { get; set; }
        public bool IsZavrsen { get; set; }
        public List<TreningVjezba> VjezbeList { get; set; }

        public Trening()
        {
            VjezbeList = new List<TreningVjezba>();
        }

    }
}
