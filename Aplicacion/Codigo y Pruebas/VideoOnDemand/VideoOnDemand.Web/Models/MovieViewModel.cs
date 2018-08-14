﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class MovieViewModel
    {
        public int? id { get; set; }

        [Required(ErrorMessage ="Debe ingresar un nombre para la película")]
        [MaxLength (100)]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [MaxLength (500)]
        [DisplayName("Descripción")]
        public string descripcion { get; set; }

        [Required(ErrorMessage ="Debe ingresar una duración para la película")]
        [Range(-1,int.MaxValue,ErrorMessage = "La duración no puede ser negativa")]
        [DisplayName("Duración(Minutos)")]
        public int? duracionMin { get; set; }

        
        [DisplayName("Fecha de registro")]
        [DataType(DataType.DateTime)]
        public DateTime? fechaRegistro { get; set; }

        [Required(ErrorMessage ="Debe insertar una fecha de lanzamiento")]
        [DisplayName("Fecha Lanzamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? fechaLanzamiento { get; set; }

        [DisplayName("Estado")]
        public EEstatusMedia? estado { get; set; }

        
        public virtual ICollection<Genero> Generos { get; set; }
        public virtual ICollection<Persona> Actores { get; set; }
        public virtual ICollection<Opinion> Opiniones { get; set; }

        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un género.")]
        public int[] SeleccionarGeneros { get; set;}

        public ICollection<PersonaViewModel> PersonasDisponibles { get; set; }
        public int[] SeleccionarPersonas { get; set; }
    }

}