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
    public class UserGlobalService : IUserGlobalRepo
    {
        private readonly Connection _connection;

        public UserGlobalService(Connection connection)
        {
            _connection = connection;
        }

        public bool EmailExists(string email)
        {
            Command command = new Command("Select Count(*) FROM [User] WHERE Email = @Email;");
            command.AddParameter("Email", email);

            int count = (int)_connection.ExecuteScalar(command);

            //bool exist = false;
            //if (count == 1)
            //{
            //    exist = true;
            //} 
            //return exist;

            return count == 1;
        }

        public UserGlobal Login(string email, string passwd)
        {
            Command command = new Command("LoginUser", true);
            command.AddParameter("Email", email);
            command.AddParameter("Passwd", passwd);

            return _connection.ExecuteReader(command, dr => dr.DBToUserGlobal()).SingleOrDefault();
        }

        public void Register(UserGlobal entity)
        {
            Command command = new Command("RegisterUser", true);
            command.AddParameter("LastName", entity.LastName);
            command.AddParameter("FirstName", entity.FirstName);
            command.AddParameter("Email", entity.Email);
            command.AddParameter("Passwd", entity.Passwd);
            _connection.ExecuteNonQuery(command);
            entity.Passwd = null;
        }
    }
}
