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
    public class SearchModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;

        public SearchModel(LionProfileService lionProfileService)
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

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            CurrentPage = pageIndex ?? 1;

            LionProfile = await _lionProfileService.SearchAsync(SearchLionTypeName, SearchWeight);
            TotalCount = LionProfile.Count;

            LionProfile = LionProfile
                .OrderByDescending(i => i.LionProfileId)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Page();
        }
    }
}
