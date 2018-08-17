using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class GeneroViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre para el género")]
        [MaxLength(50)]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        
        [MaxLength(500)]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }
    }
}