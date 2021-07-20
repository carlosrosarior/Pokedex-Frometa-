using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PokedexInfoComplete
    {
        public string NumberPo { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public List<string> TypeFName { get; set; }
        public List<string> FColor { get; set; }
        public List<string> TypeWName { get; set; }
        public List<string> WColor { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string CategoryName { get; set; }
        public string HabilityName { get; set; }
        public string FinalRowName { get; set; }
        public string back { get; set; }
        public string front { get; set; }

    }
}