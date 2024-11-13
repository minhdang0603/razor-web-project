using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWeb.Models;

namespace RazorWeb.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public readonly MyBlogContext myBlogContext;

		public IndexModel(ILogger<IndexModel> logger, MyBlogContext _myBlogContext)
		{
			_logger = logger;
			myBlogContext = _myBlogContext;
		}

		public void OnGet()
		{

			var posts = myBlogContext.Articles.OrderByDescending(p => p.Created).ToList();

			ViewData["posts"] = posts;

		}
	}
}
