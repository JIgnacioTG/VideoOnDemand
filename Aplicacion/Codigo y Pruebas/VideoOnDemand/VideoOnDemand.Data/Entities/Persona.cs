﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOnDemand.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descipcion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public ICollection<Media> Medias { get; set; }
    }
}
