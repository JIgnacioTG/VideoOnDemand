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
            //paginacion
            int totalPages = 0;
            int totalRows = 0;
            int pageSize = 3;
            int page = Request.QueryString["page"] == null ? 1 : int.Parse(Request.QueryString["page"]);
            MovieRepository repository = new MovieRepository(context);
            var lst = repository.QueryPage(null, out totalPages, out totalRows, "Nombre", page - 1, pageSize);
            var models = MapHelper.Map<IEnumerable<MovieViewModel>>(lst);
            var model = new PaginatorViewModel<MovieViewModel>
            {
                Page = page,
                TotalPages = totalPages,
                TotalRows = totalRows,
                PageSize = pageSize,
                Results = models
            };
  
            return View(model);
        }
    }
}