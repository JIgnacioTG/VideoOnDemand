using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOnDemand.Entities
{
    public class Opinion
    {
        public int Id { get; set; }
        public int? Puntuacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int? MediaId { get; set; }
        public Media Media { get; set; }
    }
}
