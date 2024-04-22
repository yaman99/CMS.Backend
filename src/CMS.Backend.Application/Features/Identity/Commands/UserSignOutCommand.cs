using CMS.Backend.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Features.Identity.Commands
{
    public class UserSignOutCommand : IRequest<Result>
    {
        public string AccessToken { get; set; }
    }
}
