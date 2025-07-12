using Repositories.Basic;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LionTypeReporitory : GenericRepository<LionType>
    {
        public LionTypeReporitory() { }

        public LionTypeReporitory(SU25LionDBContext context) => _context = context;
    }
}
