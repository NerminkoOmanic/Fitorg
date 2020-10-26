using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FITorg.Data.EntityModels
{
    public class Spol 
    {
        [Key]
        public int SpolId { get; set; }
        public string Naziv { get; set; }

    }
}
