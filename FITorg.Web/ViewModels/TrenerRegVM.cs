﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FITorg.Data.EntityModels;
using FITorg.Web.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace FITorg.Web.Models
{
    public class TrenerRegVM
    {
        public int TrenerID { get; set;}
        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$",ErrorMessage = "Email example : abc@efg.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-*.!@$%^&(){}[:;<>,.?~_+=|]).{8,32}$", 
            ErrorMessage = "At least one digit, At least one lowercase, at least one uppercase, at least one special character, at least 8 characters in length, but no more than 32")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword {get; set;}

        public DateTime DatumRodjenja { get; set; }
        public int SpolId { get; set; }
        public int GradId { get; set; }
        public int DrzavaId { get; set; }
        public List<SelectListItem> GradoviItems { get; set; }
        public List<SelectListItem> SpolItems { get; set; }

        public TrenerRegVM()
        {
            GradoviItems=new List<SelectListItem>();
            SpolItems = new List<SelectListItem>();
        }
    }
}
