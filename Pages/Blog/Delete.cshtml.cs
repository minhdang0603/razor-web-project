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
    public class DeleteModel : PageModel
    {
        private readonly RazorWeb.Models.MyBlogContext _context;

        public DeleteModel(RazorWeb.Models.MyBlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);

			if (article == null)
			{
				return NotFound();
			}
			else
			{
				Article = article;
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			if (!Guid.TryParse(id, out var guidId))
			{
				return BadRequest();
			}

			var article = await _context.Articles.FindAsync(guidId);
			if (article != null)
			{
				Article = article;
				_context.Articles.Remove(Article);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
    }
}
