using BusinessLogicLayer.Data;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ContactClientService : IContactClientRepo
    {
        private readonly IContactGlobalRepo _globalService;

        public ContactClientService(IContactGlobalRepo globalService)
        {
            _globalService = globalService;
        }

        public IEnumerable<ContactClient> Get(int userId)
        {
            return _globalService.Get(userId).Select(c => c.GlobalToClient());
        }

        public ContactClient GetById(int id)
        {
            return _globalService.GetById(id)?.GlobalToClient();
        }

        public void Insert(ContactClient contact)
        {
            _globalService.Insert(contact.ClientToGlobal());
        }

        public void Update(int id, ContactClient contact)
        {
            _globalService.Update(id, contact.ClientToGlobal());
        }
        public void Delete(int userId, int id)
        {
            _globalService.Delete(userId, id);
        }
    }
}
