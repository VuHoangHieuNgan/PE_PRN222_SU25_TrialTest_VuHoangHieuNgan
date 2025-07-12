using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LionAccountRepository : GenericRepository<LionAccount>
    {
        public LionAccountRepository()
        {

        }

        public LionAccountRepository(SU25LionDBContext context) => _context = context;

        public async Task<LionAccount?> GetUserAccount(string email, string password)
        {
            return await _context.LionAccounts
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
