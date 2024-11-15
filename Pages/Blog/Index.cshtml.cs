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

		public IList<Article> Article { get; set; } = default!;

		public const int ITEMS_PER_PAGE = 15;

		[BindProperty(SupportsGet = true, Name = "p")]
		public int currentPage { get; set; }

		public int countPage { get; set; }

		public async Task OnGetAsync(string SearchString)
		{
			//Article = await _context.Articles.ToListAsync();

			int totalArticle = await _context.Articles.CountAsync();

			countPage = (int) Math.Ceiling((double)totalArticle / ITEMS_PER_PAGE);

			if(currentPage < 1)
			{
				currentPage = 1;
			}

			if(currentPage > countPage)
			{
				currentPage = countPage;
			}

			var qr = (from a in _context.Articles
					 orderby a.Created descending
					 select a).Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE);

			if (string.IsNullOrEmpty(SearchString))
			{
				Article = await qr.ToListAsync();
			} else
			{
				Article = await qr.Where(a => a.Title.Contains(SearchString)).ToListAsync();
			}
		}
	}
}
