using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PokedexTypeWeak
    {
        public Guid Id { get; set; }
        public int Seq { get; set; }
        public Guid PokedexId { get; set; }
        public Guid TypeId { get; set; }
    }
}