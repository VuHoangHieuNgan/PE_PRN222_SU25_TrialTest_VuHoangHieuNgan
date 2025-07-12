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
        private readonly LionTypeReporitory _reporitory;
        public LionTypeService() => _reporitory ??= new LionTypeReporitory();

        public async Task<List<LionType>> GetAllAsync()
        {
            return await _reporitory.GetAllAsync();
        }
    }
}
