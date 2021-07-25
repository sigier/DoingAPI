using System;
using Doing.Common.Exceptions;

namespace Doing.Services.Doings.Domain.Models
{
    public class DoingModel
    {
        
        public Guid Id { get; protected set; }

        public Guid UserId { get; protected set; }

        public string Category { get; protected set; }
        
        public string Name { get; protected set; }

        public string Description { get; protected set; }
        
        public DateTime CreatedAt { get; protected set; }


        protected DoingModel() {}

        public DoingModel(Guid id, Guid userId, CategoryModel category, string name, string description, DateTime createdAt)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DoingException("empty_doing_name", 
                $"Doing name can not be empty.");
            }

            Id = id;
            UserId = userId;
            Category = category.Name;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}