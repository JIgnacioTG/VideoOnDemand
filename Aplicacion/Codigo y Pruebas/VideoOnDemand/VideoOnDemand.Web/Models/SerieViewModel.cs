using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class SerieViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre para la serie")]
        [DisplayName("Nombre")]
        [MaxLength(100)]
        public string nombre { get; set; }

        [DisplayName("Descripción")]
        [MaxLength(500)]
        public string descripcion { get; set; }

        [DisplayName("Duración (en Minutos)")]
        [Range(0, int.MaxValue, ErrorMessage = "La duración no puede ser negativa")]
        public int? duracionMin { get; set; }

        [DisplayName("Fecha de registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? fechaRegistro { get; set; }

        [Required(ErrorMessage = "Debe ingresar una fecha de lanzamiento")]
        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? fechaLanzamiento { get; set; }

        [DisplayName("Estado")]
        public EEstatusMedia? estado { get; set; }

        public virtual ICollection<GeneroViewModel> Generos { get; set; }
        public virtual ICollection<PersonaViewModel> Actores { get; set; }
        public virtual ICollection<OpinionViewModel> Opiniones { get; set; }

        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] GenerosSeleccionados { get; set; }

        public ICollection<PersonaViewModel> PersonasDisponibles { get; set; }
        public int[] PersonasSeleccionadas { get; set; }

        public virtual ICollection<EpisodioViewModel> Episodios { get; set; }
    }
}