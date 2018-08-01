using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOnDemand.Entities
{
    public class Media
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int? duracionMin { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public DateTime? fechaLanzamiento { get; set; }
        public EEstatusMedia? estado { get; set; }
    }
}
