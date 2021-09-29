using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Data
{
    public class ContactClient
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int UserId { get; set; }

        public ContactClient(string lastName, string firstName, string email, string phone, DateTime birthDate, int userId)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            UserId = userId;
        }

        public ContactClient(int id, string lastName, string firstName, string email, string phone, DateTime birthDate, int userId)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            UserId = userId;
        }
    }
}
