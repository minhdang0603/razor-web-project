using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorWeb.Models;

namespace RazorWeb.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly RazorWeb.Models.MyBlogContext _context;

        public IndexModel(RazorWeb.Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync(string SearchString)
        {
            var qr = from a in _context.Articles
                     orderby a.Created descending
                     select a;

            if (!string.IsNullOrEmpty(SearchString))
            {
                Article = qr.Where(a => a.Title.Contains(SearchString)).ToList();  
            } else
            {
                Article = await qr.ToListAsync();
			}
        }
    }
}
