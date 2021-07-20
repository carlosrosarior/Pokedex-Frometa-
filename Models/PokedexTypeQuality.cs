using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PokedexTypeQuality
    {
        public Guid Id { get; set; }
        public int Seq { get; set; }
        public Guid PokedexId { get; set; }
        public Guid TypeId { get; set; }
    }
}