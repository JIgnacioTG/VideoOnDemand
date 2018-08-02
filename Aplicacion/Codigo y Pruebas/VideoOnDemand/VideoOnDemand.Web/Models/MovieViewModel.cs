using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace VideoOnDemand.Web.Models
{
    public class MovieViewModel
    {
        public int? id { get; set; }

        [Required]
        [MaxLength (100)]
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [MaxLength (500)]
        [DisplayName("Descripción")]
        public string descripcion { get; set; }
        [Required]
        [DisplayName("Duración")]
        public int? duracionMin { get; set; }
        [Required]
        [DisplayName("Fecha Lanzamiento")]
        public DateTime? fechaLanzamiento { get; set; }

       
        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] SeleccionarGeneros { get; set;}

        public ICollection<PersonaViewModel> PersonasDisponibles { get; set; }
        public int[] SeleccionarPersonas { get; set; }
    }
}