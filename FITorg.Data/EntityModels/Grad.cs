using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FITorg.Data.EntityModels
{
    public class Grad
    {
        [Key]
        public int GradId { get; set; }
        public string Naziv { get; set; }
        
        [ForeignKey("DrzavaId")]
        public int DrzavaId { get; set; }
        public virtual Drzava Drzava{ get; set; }
    }
}
