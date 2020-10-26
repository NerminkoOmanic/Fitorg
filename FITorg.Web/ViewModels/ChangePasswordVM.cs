using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FITorg.Web.ViewModels
{
    public class ChangePasswordVM
    {
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string StariPW {get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-*.!@$%^&(){}[:;<>,.?~_+=|]).{8,32}$", 
            ErrorMessage = "At least one digit, At least one lowercase, at least one uppercase, at least one special character, at least 8 characters in length, but no more than 32")]
        public string NoviPW { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NoviPW", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword {get; set;}
    }
}
