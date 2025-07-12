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

        public async Task<List<LionProfile>> SearchAsync(string lionTypeName, double? weight, int pageNumber, int pageSize)
        {
            return await _context.LionProfiles
                .Include(i => i.LionType)
                .Where(i =>
                    (string.IsNullOrEmpty(lionTypeName) || i.LionType.LionTypeName.Contains(lionTypeName))
                    && (weight == null || i.Weight == weight)
                )
                .OrderByDescending(a => a.LionProfileId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync() ?? new List<LionProfile>();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.LionProfiles.CountAsync();
        }

        public async Task<int> GetSearchCountAsync(string lionTypeName, double? weight)
        {
            return await _context.LionProfiles
                .Include(i => i.LionType)
                .Where(i =>
                    (string.IsNullOrEmpty(lionTypeName) || i.LionType.LionTypeName.Contains(lionTypeName))
                    && (weight == null || i.Weight == weight)
                )
                .CountAsync();
        }
    }
}
