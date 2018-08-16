using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class OpinionViewModel
    {
        public int Id { get; set; }

        [Required]
        public int? Puntuacion { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime? FechaRegistro { get; set; }

        [Required]
        public int? UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public string IdentityId { get; set; }

        [Required]
        public int? MediaId { get; set; }

        public Media Media { get; set; }
    }
}