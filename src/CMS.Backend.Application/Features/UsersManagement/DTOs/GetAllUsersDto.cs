using AutoMapper;
using CMS.Backend.Application.Common.Mappings;
using CMS.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Features.UsersManagement.DTOs
{
    public class GetAllUsersDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Email { get;  set; }
        public string Name { get;  set; }
        public string UserType { get; set; }
        public bool IsActive { get;  set; }
        //public bool IsCompleted { get; set; }

    }
}
