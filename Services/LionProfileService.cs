﻿using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LionProfileService
    {
        private readonly LionProfileRepository _repository;

        public LionProfileService() => _repository ??= new LionProfileRepository();

        public async Task<List<LionProfile>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<LionProfile> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(LionProfile item)
        {
            return await _repository.CreateAsync(item);
        }

        public async Task<int> UpdateAsync(LionProfile item)
        {
            var result = await _repository.UpdateAsync(item);
            await _repository.SaveAsync();
            return result;
        }

        public async Task<bool> DeleteAsync(int code)
        {
            var item = await _repository.GetByIdAsync(code);

            if (item == null)
            {
                return false;
            }

            return await _repository.RemoveAsync(item);
        }
    }
}
