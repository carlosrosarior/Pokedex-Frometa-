using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PokedexInfoNC
    {
        public string NumberPo { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public List<string> TypeName { get; set; }
        public List<string> Color { get; set; }
    }
}