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
    public class LionProfileRepository : GenericRepository<LionProfile>
    {
        public LionProfileRepository()
        {

        }

        public LionProfileRepository(SU25LionDBContext context) => _context = context;

        public override async Task<List<LionProfile>> GetAllAsync()
        {
            return await _context.LionProfiles
                .Include(p => p.LionType)
                .ToListAsync() ?? new List<LionProfile>();
        }

        public override async Task<LionProfile> GetByIdAsync(int id)
        {
            return await _context.LionProfiles
                .Include(p => p.LionType)
                .FirstOrDefaultAsync(p => p.LionProfileId == id) ?? new LionProfile();
        }

        public (List<LionProfile> list, int totalCount) GetPaginatedProfiles(int pageIndex, int pageSize)
        {
            var list = _context.LionProfiles
                .Include(i => i.LionType)
                .OrderByDescending(i => i.LionProfileId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList() ?? new List<LionProfile>();
            var totalCount = _context.LionProfiles.Count();

            return (list, totalCount);
        }
    }
}
