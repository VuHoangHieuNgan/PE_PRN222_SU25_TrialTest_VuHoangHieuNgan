using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionPetManagement_VuHoangHieuNgan.Pages.LionProfiles
{
    [Authorize(Roles = "2, 3")]
    public class IndexModel : PageModel
    {
        private readonly Repositories.Models.SU25LionDBContext _context;

        public IndexModel(Repositories.Models.SU25LionDBContext context)
        {
            _context = context;
        }

        public IList<LionProfile> LionProfile { get;set; } = default!;

        public async Task OnGetAsync()
        {
            LionProfile = await _context.LionProfiles
                .Include(l => l.LionType).ToListAsync();
        }
    }
}
