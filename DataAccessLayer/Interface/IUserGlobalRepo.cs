using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IUserGlobalRepo
    {
        UserGlobal Login(string email, string passwd);
        void Register(UserGlobal entity);
        bool EmailExists(string email);
    }
}
