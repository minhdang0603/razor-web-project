using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWeb.Pages.Shared
{
    public class _PagingModel : PageModel
    {
        public int currentPage { get; set; }

        public int countPages { get; set; }

        public Func<int?, string> generateUrl { get; set; }

        public void OnGet()
        {
        }
    }
}
