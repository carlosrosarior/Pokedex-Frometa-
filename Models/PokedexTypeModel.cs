using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PokedexTypeModel
    {
      
        public Guid Id { get; set; }
        public int Seq { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}