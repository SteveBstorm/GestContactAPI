using BusinessLogicLayer.Data;
using GestContactAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestContactAPI.Tools
{
    public static class Mappers
    {
        public static UserClient toBll(this RegisterForm form)
        {
            return new UserClient(form.LastName, form.FirstName, form.Email, form.Passwd);
        }

        public static ContactClient toBll(this ContactForm form)
        {
            return new ContactClient(form.LastName, form.FirstName, form.Email, form.Phone, form.BirthDate, 2);
        }

        public static ContactClient toBll(this UpdateContactForm form)
        {
            return new ContactClient(form.Id,form.LastName, form.FirstName, form.Email, form.Phone, form.BirthDate, 2);
        }
    }
}
