using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class FavoritoViewModel
    {
        public int id { get; set; }
        public DateTime? FechaAgregado { get; set; }
        public int? usuarioId { get; set; }
        public Usuario usuario { get; set; }
        public int? mediaId { get; set; }
        public Media media { get; set; }
    }
}