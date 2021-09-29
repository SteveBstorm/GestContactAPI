using BusinessLogicLayer.Data;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Interface;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserClientService : IUserClientRepo
    {
        private readonly IUserGlobalRepo _userGlobalService;

        public UserClientService(IUserGlobalRepo service)
        {
            _userGlobalService = service;
        }

        public bool EmailExists(string email)
        {
            return _userGlobalService.EmailExists(email);
        }

        public UserClient Login(string email, string passwd)
        {
            return _userGlobalService.Login(email, passwd)?.GlobalToClient();
        }

        public void Register(UserClient entity)
        {
            _userGlobalService.Register(entity.ClientToGlobal());
        }
    }
}
