using BusinessLogicLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserClientRepo
    {
        UserClient Login(string email, string passwd);
        void Register(UserClient entity);
        bool EmailExists(string email);
    }
}
