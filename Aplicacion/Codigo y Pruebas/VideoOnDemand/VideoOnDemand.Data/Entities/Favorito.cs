using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOnDemand.Entities
{
    public class Favorito
    {
        public int id { get; set; }
        public DateTime? FechaAgregado { get; set; }
        public int? usuarioId { get; set; }
        public Usuario usuario { get; set; }
        public int? mediaId { get; set; }
        public Media media { get; set; }
    }
}
