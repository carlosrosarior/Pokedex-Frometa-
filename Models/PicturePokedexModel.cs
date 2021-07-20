using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PicturePokedexModel
    {
      
        public Guid Id { get; set; }
        public int Seq { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}