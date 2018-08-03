using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class EpisodioViewModel : MediaViewModel
    {
        [DisplayName("Temporada")]
        public int? temporada { get; set; }

        public int? serieId { get; set; }

        public Serie Serie { get; set; }
    }
}