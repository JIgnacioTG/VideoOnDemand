using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class OpinionViewModel
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