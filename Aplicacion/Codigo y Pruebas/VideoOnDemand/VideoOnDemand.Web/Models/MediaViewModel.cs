using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class MediaViewModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int? duracionMin { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public DateTime? fechaLanzamiento { get; set; }
        public EEstatusMedia? estado { get; set; }
    }
}