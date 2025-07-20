using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionPetManagement_VuHoangHieuNgan.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToPage("/LionProfiles/Index");
            }

            return RedirectToPage("/Authentication/Login");
        }
    }
}
