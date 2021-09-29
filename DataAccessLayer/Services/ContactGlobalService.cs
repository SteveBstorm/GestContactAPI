using ConnectionTool;
using DataAccessLayer.Data;
using DataAccessLayer.Interface;
using DataAccessLayer.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class ContactGlobalService : IContactGlobalRepo
    {
        private readonly Connection _connection;

        public ContactGlobalService(Connection connection)
        {
            _connection = connection;
        }




        public IEnumerable<ContactGlobal> Get(int userID)
        {
            Command command = new Command("SELECT * FROM CONTACT WHERE UserId = @UserId");
            command.AddParameter("UserId", userID);
            return _connection.ExecuteReader(command, dr => dr.DBToContactGlobal());
        }

        public ContactGlobal GetById(int contactId)
        {
            Command command = new Command("SELECT * FROM CONTACT WHERE Id=@Id");
            command.AddParameter("Id", contactId);
            return _connection.ExecuteReader(command, dr => dr.DBToContactGlobal()).SingleOrDefault();
        }

        public void Insert(ContactGlobal contact)
        {
            Command command = new Command("AddContact", true);
            command.AddParameter("LastName", contact.LastName);
            command.AddParameter("FirstName", contact.FirstName);
            command.AddParameter("Email", contact.Email);
            command.AddParameter("Phone", contact.Phone);
            command.AddParameter("BirthDate", contact.BirthDate);
            command.AddParameter("UserId", contact.UserId);

            _connection.ExecuteNonQuery(command);
        }

        public void Update(int id, ContactGlobal contact)
        {
            Command command = new Command("UpdateContact", true);
            command.AddParameter("Id", id);
            command.AddParameter("LastName", contact.LastName);
            command.AddParameter("FirstName", contact.FirstName);
            command.AddParameter("Email", contact.Email);
            command.AddParameter("Phone", contact.Phone);
            command.AddParameter("BirthDate", contact.BirthDate);
            command.AddParameter("UserId", contact.UserId);

            _connection.ExecuteNonQuery(command);
        }

        public void Delete(int userId, int contactId)
        {
            Command command = new Command("DeleteContact", true);
            command.AddParameter("Id", contactId);

            _connection.ExecuteNonQuery(command);
        }
    }
}
