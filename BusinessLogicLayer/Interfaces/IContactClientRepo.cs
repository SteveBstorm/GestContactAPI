using BusinessLogicLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IContactClientRepo
    {
        IEnumerable<ContactClient> Get(int userId);
        ContactClient GetById(int id);
        void Insert(ContactClient contact);
        void Update(int id, ContactClient contact);
        void Delete(int userId, int id);
    }
}
