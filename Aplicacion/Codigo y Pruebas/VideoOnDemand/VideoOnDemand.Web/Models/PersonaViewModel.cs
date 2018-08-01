using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class PersonaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descipcion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
}