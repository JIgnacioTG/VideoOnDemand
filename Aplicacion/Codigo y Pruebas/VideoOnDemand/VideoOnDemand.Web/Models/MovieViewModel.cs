using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VideoOnDemand.Entities;

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

        
        [DisplayName("Fecha de registro")]
        [DataType(DataType.DateTime)]
        public DateTime? fechaRegistro { get; set; }

        [Required]
        [DisplayName("Fecha Lanzamiento")]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? fechaLanzamiento { get; set; }

        [DisplayName("Estado")]
        public EEstatusMedia? estado { get; set; }

        public ICollection<Genero> Generos { get; set; }
        public ICollection<Persona> Actores { get; set; }
        public ICollection<Opinion> Opiniones { get; set; }

        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] SeleccionarGeneros { get; set;}

        public ICollection<PersonaViewModel> PersonasDisponibles { get; set; }
        public int[] SeleccionarPersonas { get; set; }
    }

}