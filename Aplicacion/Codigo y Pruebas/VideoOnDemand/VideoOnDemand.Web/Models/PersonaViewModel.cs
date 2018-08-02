using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class PersonaViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }
    }
}