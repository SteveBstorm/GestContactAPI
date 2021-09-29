using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Data
{
    public class UserClient
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }

        public UserClient(string lastName, string firstName, string email, string passwd)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Passwd = passwd;
        }

        public UserClient(int id, string lastName, string firstName, string email, string passwd)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Passwd = passwd;
        }
    }
}
