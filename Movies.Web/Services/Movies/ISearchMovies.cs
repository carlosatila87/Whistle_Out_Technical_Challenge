using Movies.Web.Models.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Web.Services.Movies
{
    public interface ISearchMovies
    {
        Task<ResultCollection> GetMovies(string title, string type, string year, int page);
        Task<Details> GetMovieDetails(string imdbId);
    }
}
