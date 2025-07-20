using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LionTypeService
    {
        private readonly LionTypeRepository _repository;
        public LionTypeService() => _repository ??= new LionTypeRepository();

        public async Task<List<LionType>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
