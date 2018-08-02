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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaNacimiento { get; set; }

    }
}