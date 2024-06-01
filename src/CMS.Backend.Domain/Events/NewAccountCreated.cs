using CMS.Backend.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Domain.Events
{
    public class NewAccountCreated : DomainEvent
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public NewAccountCreated(string email , string password)
        {
            Password = password;
            Email = email;
        }
    }
}
