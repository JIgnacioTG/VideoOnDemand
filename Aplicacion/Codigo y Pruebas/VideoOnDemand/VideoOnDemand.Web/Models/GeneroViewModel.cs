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

        [Required]
        [MaxLength(50)]
        [DisplayName("Nombre del género")]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Descripción del género")]
        public string Descripcion { get; set; }
    }
}