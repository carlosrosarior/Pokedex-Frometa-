using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class PokedexModel
    {
        public Guid Id { get; set; }
        public int Seq { get; set; }
        public string NumberPo { get; set; }
        public string Name { get; set; }
        public Guid PictureId { get; set; }
        public Guid CategoryId { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public Guid HabilityId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}