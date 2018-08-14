using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class MovieSerieViewModel
    {
        public ICollection<MovieViewModel> Movies { get; set; }

        public ICollection<SerieViewModel> Series { get; set; }
    }
}