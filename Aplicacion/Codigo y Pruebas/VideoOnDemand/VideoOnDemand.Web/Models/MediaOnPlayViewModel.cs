using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class MediaOnPlayViewModel
    {
        public int Id { get; set; }
        public long? Milisegundo { get; set; }
        public int? MediaId { get; set; }
        public MediaViewModel Media { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioViewModel Usuario { get; set; }
    }
}