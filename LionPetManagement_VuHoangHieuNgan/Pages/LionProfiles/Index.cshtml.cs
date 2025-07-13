using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionPetManagement_VuHoangHieuNgan.Pages.LionProfiles
{
    [Authorize(Roles = "2, 3")]
    public class IndexModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;

        public IndexModel(LionProfileService lionProfileService)
        {
            _lionProfileService = lionProfileService;
        }

        public IList<LionProfile> LionProfile { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public String SearchLionTypeName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public double? SearchWeight { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public IActionResult OnGetAsync(int? pageIndex)
        {
            CurrentPage = pageIndex ?? 1;

            var (list, count) = _lionProfileService.GetPaginatedProfiles(CurrentPage, PageSize);
            LionProfile = list;
            TotalCount = count;
            return Page();
        }
    }
}
