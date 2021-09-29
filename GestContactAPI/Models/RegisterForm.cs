using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GestContactAPI.Models
{
    public class RegisterForm
    {
        [DisplayName("Nom : ")]
        [Required(ErrorMessage = "Le champs 'LastName' est requis")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "La longueur du champs 'LastName' doit être compris entre 1 et 50 caractères")]
        public string LastName { get; set; }
        [DisplayName("Prénom : ")]
        [Required(ErrorMessage = "Le champs 'FirstName' est requis")]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }
        [DisplayName("Email : ")]
        [Required(ErrorMessage = "Le champs 'Email' est requis")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Mot de passe : ")]
        [Required(ErrorMessage = "Le champs 'Password' est requis")]
        [StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.*(?=.{8,20})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+-=]).*$", ErrorMessage = "Votre mot de passe n'est pas assez fort")]
        public string Passwd { get; set; }
        [DisplayName("Confirmation : ")]
        [DataType(DataType.Password)]
        [Compare(nameof(Passwd))]
        public string Confirm { get; set; }
    }
}
