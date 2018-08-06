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
        [MaxLength(100)]
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        [MaxLength(500)]
        public string Descripcion { get; set; }

        [DisplayName("Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }

        public bool Eliminado { get; set; }
    }
}