using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PokedexHability
    {
        public Guid Id { get; set; }
        public int Seq { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}