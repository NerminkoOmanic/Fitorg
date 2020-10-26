using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FITorg.Data.EntityModels
{
    public class Administrator
    {
        public int AdministratorId { get; set; }
        public string IzjavaOZastitiPodatak { get; set; }

        [ForeignKey("AppUserId")]
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
