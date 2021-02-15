using Microsoft.Extensions.Configuration;
using Movies.Web.Models.Movies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Movies.Web.Services.Movies
{
    public class SearchMovies : ISearchMovies
    {
        private readonly IConfiguration _configuration;
        private string APIKEY;
        public SearchMovies(IConfiguration configuration)
        {
            _configuration = configuration;
            APIKEY = _configuration["OMDB:ApiKey"];
        }

        public async Task<ResultCollection> GetMovies(string title, string type, string year, int page)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var queryUrl = $"http://www.omdbapi.com/?apikey={APIKEY}&s={HttpUtility.UrlEncode(title)}";
                if (!string.IsNullOrEmpty(type))
                    queryUrl += $"&type={type}";
                if (!string.IsNullOrEmpty(year))
                    queryUrl += $"&y={HttpUtility.UrlEncode(year)}";
                if (page > 1)
                    queryUrl += $"&page={page}";
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(queryUrl))
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var jsonResponse = JObject.Parse(apiResponse);
                            if ((bool)jsonResponse["Response"] == true)
                                return jsonResponse.ToObject<ResultCollection>();
                            else
                                throw new HttpRequestException((string)jsonResponse["Error"]);
                        }
                    }
                }
                catch { }
            }
            return null;
        }

        public async Task<Details> GetMovieDetails(string imdbId)
        {
            if (!string.IsNullOrEmpty(imdbId))
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync($"http://www.omdbapi.com/?apikey={APIKEY}&i={imdbId}"))
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var jsonResponse = JObject.Parse(apiResponse);
                            if ((bool)jsonResponse["Response"] == true)
                                return jsonResponse.ToObject<Details>();
                            else
                                throw new HttpRequestException((string)jsonResponse["Error"]);
                        }
                    }
                }
                catch { }
            }
            return null;
        }
    }
}