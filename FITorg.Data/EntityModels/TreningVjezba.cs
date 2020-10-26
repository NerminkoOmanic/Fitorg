using System.ComponentModel.DataAnnotations.Schema;

namespace FITorg.Data.EntityModels
{
    
    public class TreningVjezba
    {
        
        public int TreningVjezbaId { get; set; }

        [ForeignKey("TreningId")]
        public int TreningId { get; set; }
        public virtual Trening Trening { get; set; }

        [ForeignKey("VjezbaId")]
        public int VjezbaId { get; set; }
        public virtual Vjezba Vjezba { get; set; }

        public int BrojSerija { get; set; }
        public int BrojPonavljanja { get; set; }
        public float Opeterecenje { get; set; }
        public string Komentar{ get; set; }


    }
}
