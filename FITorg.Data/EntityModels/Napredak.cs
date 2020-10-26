using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FITorg.Data.EntityModels
{
    public class Napredak
    {
        [Key]
        public int NapredakId { get; set; }
        [ForeignKey("SportasId")]
        public int SportasId { get; set; }
        public virtual Sportas Sportas { get; set; }
        public DateTime DatumMjerenja { get; set; }
        public float Tezina { get; set; }
        public string UdioMasti { get; set; }

    }
}
