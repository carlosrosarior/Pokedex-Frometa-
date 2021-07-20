using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pokedex.Data
{
    public class PokedexContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PokedexContext() : base("name=PokedexContext")
        {
        }

        public System.Data.Entity.DbSet<Pokedex.Models.PokedexTypeModel> PokedexTypeModels { get; set; }

        public System.Data.Entity.DbSet<Pokedex.Models.PokedexModel> PokedexModels { get; set; }
    }
}
