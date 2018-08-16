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

        [Required]
        [DisplayName("Nombre")]
        [MaxLength(100)]
        public string nombre { get; set; }

        [Required]
        [DisplayName("Descripción")]
        [MaxLength(500)]
        public string descripcion { get; set; }

        [DisplayName("Duración(Minutos)")]
        [Range(0, int.MaxValue, ErrorMessage = "La duración no puede ser negativa")]
        [DataType(DataType.Duration)]
        public int? duracionMin { get; set; }

        [DisplayName("Fecha registro")]
        [DataType(DataType.Date)]
        public DateTime? fechaRegistro { get; set; }

        [Required]
        [DisplayName("Fecha lanzamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? fechaLanzamiento { get; set; }

        public virtual ICollection<GeneroViewModel> Generos { get; set; }
        public virtual ICollection<PersonaViewModel> Actores { get; set; }
        public virtual ICollection<OpinionViewModel> Opiniones { get; set; }

        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] GenerosSeleccionados { get; set; }

        public ICollection<PersonaViewModel> PersonasDisponibles { get; set; }
        public int[] PersonasSeleccionadas { get; set; }

        [DisplayName("Estado")]
        public EEstatusMedia? estado { get; set; }

        public virtual ICollection<EpisodioViewModel> Episodios { get; set; }
    }
}