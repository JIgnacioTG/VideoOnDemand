using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class MediaViewModel
    {
        public int id { get; set; }

        [Required]
        [DisplayName("Nombre")]
        [MaxLength(100)]
        public string nombre { get; set; }

        [DisplayName("Descripción")]
        [MaxLength(500)]
        public string descripcion { get; set; }

        [Required]
        [DisplayName("Duración")]
        [DataType(DataType.Duration)]
        public int? duracionMin { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Fecha registro")]
        public DateTime? fechaRegistro { get; set; }

        [DisplayName("Fecha lanzamiento")]
        [DataType(DataType.Date)]
        public DateTime? fechaLanzamiento { get; set; }

        [Required]
        [DisplayName("Estado")]
        public EEstatusMedia? estado { get; set; }
    }
}