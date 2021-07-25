using System;
using Doing.Common.Exceptions;
using Doing.Services.Identity.Domain.Services;

namespace Doing.Services.Identity.Domain.Models
{
    public class User
    {
        

        public Guid Id { get; protected set; }

         public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string Name { get; protected set; }

        public string Salt { get; protected set; }

        public DateTime CreatedAt {get; protected set;}

        protected  User() {}       
        
        public User(string email, string name)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new DoingException("empty_email", 
                $"Email field can not be empty.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new DoingException("empty_name", 
                $"Name field can not be empty.");
            }

            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new DoingException("empty_password", 
                $"No password provided.");
            }

            Salt = encrypter.GetSalt();

            Password = encrypter.GetHash(password, Salt);
        }

        public bool IsPasswordValid(string password, IEncrypter encrypter)
        {
            return Password.Equals(encrypter.GetHash(password, Salt));
        }
    }
}
