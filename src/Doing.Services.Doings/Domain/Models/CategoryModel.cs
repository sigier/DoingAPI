using System;

namespace Doing.Services.Doings.Domain.Models
{
    public class CategoryModel
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        protected CategoryModel() {}

        public CategoryModel(string name)
        {
            Id = Guid.NewGuid();
            Name = name.ToLowerInvariant();
        }
    }
}