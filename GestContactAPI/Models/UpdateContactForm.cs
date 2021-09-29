using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestContactAPI.Models
{
    public class UpdateContactForm
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        [DisplayName("Nom : ")]
        public string LastName { get; set; }
        [Required]
        [StringLength(75)]
        [DisplayName("Prenom : ")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(384)]
        [EmailAddress]
        [DisplayName("Email : ")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Téléphone : ")]
        public string Phone { get; set; }

        [Required]
        [DisplayName("Date de naissance : ")]
        public DateTime BirthDate { get; set; }
    }
}
