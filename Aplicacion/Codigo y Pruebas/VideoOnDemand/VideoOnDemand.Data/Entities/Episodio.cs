using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOnDemand.Entities
{
    public class Episodio : Media
    {
        public int? temporada { get; set; }
        public int? serieId { get; set; }
        public virtual Serie Serie { get; set; }
    }
}
