using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class MovieController : BaseController
    {
        // GET: Movie
        public ActionResult Index()
        {
            MovieRepository repository = new MovieRepository(context);
            
            var lst = repository.GetAll();
            lst.ElementAt(0);
            var models = MapHelper.Map<IEnumerable<MovieViewModel>>(lst);
            return View(models);
        }
    }
}