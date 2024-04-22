using CMS.Backend.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend   .Appilication.Features.UsersManagement.Commands
{
    public class AddNewCustomerUserCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool HasProfile { get; set; }
        public Guid ProfileId { get; set; }
        public string Phone { get; set; }
        public bool IsCompany { get; set; }
    }
}
