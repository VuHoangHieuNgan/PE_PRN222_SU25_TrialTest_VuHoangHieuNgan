using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LionAccountService
    {
        private readonly LionAccountRepository _repository;

        public LionAccountService() => _repository ??= new LionAccountRepository();

        public async Task<LionAccount?> GetUserAccountAsync(string email, string password)
        {
            return await _repository.GetUserAccount(email, password);
        }
    }
}
