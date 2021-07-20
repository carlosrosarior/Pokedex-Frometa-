using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PokedexAll
    {
        public PokedexModel SelectPokemon { get; set; }
        public PokedexTypeQuality SelectTypeQuality { get; set; }
        public PokedexTypeWeak SelectTypeWeak { get; set; }
        public IEnumerable<PicturePokedexModel> SelectPicture { get; set; }
        public IEnumerable<PokedexCategory> SelectCategory { get; set; }
        public IEnumerable<PokedexHability> SelectHability { get; set; }
        public IEnumerable<PokedexTypeModel> SelectType { get; set; }
        public IEnumerable<PokedexModel>SelectPokemons { get; set; }
    }
}