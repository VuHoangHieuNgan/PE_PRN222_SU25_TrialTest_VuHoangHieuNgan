using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionPetManagement_VuHoangHieuNgan.Pages.LionProfiles
{
    [Authorize(Roles = "2")]
    public class CreateModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;
        private readonly LionTypeService _lionTypeService;

        public CreateModel(LionProfileService lionProfileService, LionTypeService lionTypeService)
        {
            _lionProfileService = lionProfileService;
            _lionTypeService = lionTypeService;
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadDropdownData();
            LionProfile = new LionProfile
            {
                ModifiedDate = DateTime.Now
            };

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownData();
                return Page();
            }

            await _lionProfileService.CreateAsync(LionProfile);

            return RedirectToPage("./Index");
        }

        private async Task LoadDropdownData()
        {
            var list = await _lionTypeService.GetAllAsync();
            var selectList = list.Select(l => new SelectListItem
            {
                Value = l.LionTypeId.ToString(),
                Text =  $"{l.LionTypeId} - {l.LionTypeName}"
            }).ToList();
            ViewData["LionTypeId"] = new SelectList(selectList, "Value", "Text");
        }
    }
}
