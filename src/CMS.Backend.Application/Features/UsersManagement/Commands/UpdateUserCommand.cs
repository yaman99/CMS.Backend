﻿using AutoMapper;
using CMS.Backend.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Features.UsersManagement.Commands
{
    public class UpdateUserCommand : IRequest<Result> 
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
  

    }
}
