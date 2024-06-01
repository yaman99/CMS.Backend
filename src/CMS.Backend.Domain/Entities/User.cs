using System;
using System.Collections.Generic;
using System.Linq;
using CMS.Backend.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace CMS.Backend.Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string PasswordHash { get; private set; }
        public string UserType { get; set; }
        public string ActivationCode { get;  private set; }
        public bool IsActive { get; private set; }
        public bool IsCompleted { get; set; }
        public RefreshToken RefreshToken { get; private set; }



        public User(Guid id):base(id)
        {
        }

        public User(Guid id, string email , string userType, string name) : base(id)
        {
            Id = id;
            Email = email.ToLowerInvariant();
            IsActive = true;
            IsCompleted = false;
            UserType = userType.ToLowerInvariant();
            Name = name;
            Phone = string.Empty;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new DomainException(Codes.InvalidPassword, 
                    "Password can not be empty.");
            }             
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public void GenerateRefreshToken(IPasswordHasher<User> passwordHasher)
        {
            RefreshToken = new RefreshToken(this, passwordHasher);
            
        }
       
        public void GenerateActivationCode()
        {
            ActivationCode = Guid.NewGuid().ToString("N");
        }
        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, PasswordHash, password) != PasswordVerificationResult.Failed;

        public void ActivateAccount()=>this.IsActive = true;
        public void DeActivateAccount() => this.IsActive = false;

        public string GenerateStampPassword()
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}