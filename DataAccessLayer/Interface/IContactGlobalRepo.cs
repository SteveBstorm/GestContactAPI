using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IContactGlobalRepo
    {
        IEnumerable<ContactGlobal> Get(int userID);
        ContactGlobal GetById(int contactId);
        void Insert(ContactGlobal contact);
        void Update(int id, ContactGlobal contact);
        void Delete(int userId, int contactId);
    }
}
