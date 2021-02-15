using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models.Movies
{
    public class ResultCollection
    {
        public List<SearchResult> Search { get; set; }
        public int TotalResults { get; set; }
    }
}
