using Microsoft.AspNetCore.Mvc;
using Movies.Web.Services.Movies;
using System.Threading.Tasks;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ISearchMovies _searchMovies;
        public MoviesController(ISearchMovies searchMovies)
        {
            _searchMovies = searchMovies;
        }
        public IActionResult Index()
        {            
            return View();
        }

        public async Task<IActionResult> SearchResult(string s, string type, string y, int page = 1)
        {
            var model = await _searchMovies.GetMovies(s, type, y, page);
            if (model == null)
                return View(model);
            var totalPages = (model.TotalResults % 10 > 0) ? (model.TotalResults / 10) + 1 : (model.TotalResults / 10);
            var previousPage = page - 1;
            var nextPage = page == totalPages ? 0 : page + 1;
            ViewData["PreviousPage"] = previousPage;
            ViewData["NextPage"] = nextPage;
            return View(model);
        }

        public async Task<IActionResult> Details(string i)
        {
            var model = await _searchMovies.GetMovieDetails(i);
            return View(model);
        }
    }
}