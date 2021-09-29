using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mapper
{
    internal static class Mapper
    {
        internal static UserGlobal DBToUserGlobal(this IDataRecord dataRecord)
        {
            return new UserGlobal()
            {
                Id = (int)dataRecord["Id"],
                LastName = dataRecord["LastName"].ToString(),
                //ToString() et (string) feront le même résutat 
                FirstName = (string)dataRecord["FirstName"],
                Email = (string)dataRecord["Email"],
                Passwd = null, //on ne renvoit jamais un mot de passe d'une base de données
            };
        }

        internal static ContactGlobal DBToContactGlobal(this IDataRecord dataRecord)
        {
            return new ContactGlobal()
            {
                Id = (int)dataRecord["Id"],
                LastName = (string)dataRecord["LastName"],
                FirstName = (string)dataRecord["FirstName"],
                Email = (string)dataRecord["Email"],
                Phone = (string)dataRecord["Phone"],
                BirthDate = (DateTime)dataRecord["BirthDate"],
                UserId = (int)dataRecord["UserId"]
            };
        }
    }
}
