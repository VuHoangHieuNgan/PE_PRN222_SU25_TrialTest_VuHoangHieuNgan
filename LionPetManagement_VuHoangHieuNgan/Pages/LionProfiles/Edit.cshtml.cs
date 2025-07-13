using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionPetManagement_VuHoangHieuNgan.Pages.LionProfiles
{
    [Authorize(Roles = "2")]
    public class EditModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;
        private readonly LionTypeService _lionTypeService;

        public EditModel(LionProfileService lionProfileService, LionTypeService lionTypeService)
        {
            _lionProfileService = lionProfileService;
            _lionTypeService = lionTypeService;
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile =  await _lionProfileService.GetByIdAsync(id.Value);
            if (lionprofile == null)
            {
                return NotFound();
            }
            LionProfile = lionprofile;

            await LoadDropdownData();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _lionProfileService.UpdateAsync(LionProfile);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LionProfileExists(LionProfile.LionProfileId).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> LionProfileExists(int id)
        {
            var result = await _lionProfileService.GetByIdAsync(id);
            return result != null && result.LionProfileId == id;
        }

        private async Task LoadDropdownData()
        {
            var lionTypes = await _lionTypeService.GetAllAsync();
            var lionTypeSelectList = lionTypes.Select(l => new SelectListItem
            {
                Value = l.LionTypeId.ToString(),
                Text = $"{l.LionTypeId} - {l.LionTypeName}"
            }).ToList();
            ViewData["LionTypeId"] = new SelectList(lionTypeSelectList, "Value", "Text");
        }
    }
}
