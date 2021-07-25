using System.Diagnostics;
using System;
using System.Threading.Tasks;
using Doing.Common.Exceptions;
using Doing.Services.Doings.Domain.Repositories;
using Doing.Services.Doings.Domain.Models;

namespace Doing.Services.Doings.Services
{
    public class DoingService : IDoingService
    {
        private readonly IDoingRepository _doingRepository;
        
        private readonly ICategoryRepository _categoryRepository;

        public DoingService(IDoingRepository doingRepository, ICategoryRepository categoryRepository)
        {
            _doingRepository = doingRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var doingCategory = await _categoryRepository.GetAsync(name);

            if (doingCategory == null)
            {
                throw new DoingException("category_does_not_exist", 
                $"Category {category} was not found.");
            }

            var doing = new DoingModel(id, userId, doingCategory,  name, description, createdAt);

            await _doingRepository.AddAsync(doing);
        }
    }
}