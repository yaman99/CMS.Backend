﻿using CMS.Backend.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Features.Identity.Commands
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public Guid UserId { get; }
        public string CurrentPassword { get; }
        public string NewPassword { get; }
        public string PasswordConfirmation { get; }

        [JsonConstructor]
        public ChangePasswordCommand(
            Guid userId,
            string currentPassword,
            string newPassword,
            string passwordConfirmation)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            PasswordConfirmation = passwordConfirmation;
        }
    }
}
