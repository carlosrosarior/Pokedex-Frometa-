using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PrincipalPokedexInfo
    {
        public IEnumerable<PokedexModel> SelectPokemons { get; set; }
        public PokedexModel Pokemon { get; set; }
    }
}