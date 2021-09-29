using BusinessLogicLayer.Data;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Mappers
{
    internal static class Mapper
    {
        internal static UserGlobal ClientToGlobal(this UserClient entity)
        {
            return new UserGlobal()
            {
                Id = entity.Id,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Email = entity.Email,
                Passwd = entity.Passwd
            };
        }
        internal static UserClient GlobalToClient(this UserGlobal entity)
        {
            return new UserClient(entity.Id, entity.LastName, entity.FirstName, entity.Email, entity.Passwd);
        }

        internal static ContactGlobal ClientToGlobal(this ContactClient entity)
        {
            return new ContactGlobal()
            {
                Id = entity.Id,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Email = entity.Email,
                Phone = entity.Phone,
                BirthDate = entity.BirthDate,
                UserId = entity.UserId
            };
        }

        internal static ContactClient GlobalToClient(this ContactGlobal entity)
        {
            return new ContactClient(
                    entity.Id, 
                    entity.LastName, 
                    entity.FirstName, 
                    entity.Email, 
                    entity.Phone,
                    entity.BirthDate,
                    entity.UserId
                );
        }
    }
}
