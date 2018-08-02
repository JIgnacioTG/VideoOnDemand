using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class SerieViewModel : MediaViewModel
    {

        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] GenerosSeleccionados { get; set; }

        public ICollection<PersonaViewModel> PersonasDisponibles { get; set; }
        public int[] PersonasSeleccionadas { get; set; }

    }
}