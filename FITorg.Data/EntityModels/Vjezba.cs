using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FITorg.Data.EntityModels
{
    public class Vjezba 
    {
        [Key]
        public int VjezbaId { get; set; }
        
        [ForeignKey("TrenerId")]
        public int TrenerId { get; set; }
        public virtual Trener Trener { get; set; }
        public string Naziv{ get; set; }
        public string VideoUrl { get; set; }
    }
}
