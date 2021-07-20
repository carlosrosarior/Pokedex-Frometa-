using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Pokedex.Models;

namespace Pokedex.Controllers
{
    public class HomeController : Controller
    {
        readonly string connString = "Server=DESKTOP-453205K;Database=Pokemon;Trusted_Connection=True;";
        public ActionResult Index()
        {
            //pokedex.SelectPictures = SpecificPokemonPicture(pokedex.SelectPokemons);
            //pokedex.SelectQuality = SelectPokemonQualities(pokedex.SelectPokemons);
            //pokedex.selectTypes = SelectPokemonTypes(pokedex.SelectQuality);
            PrincipalPokedexInfo pokedex = new PrincipalPokedexInfo();
            pokedex.SelectPokemons = GetRecords();
            PokedexInfoNC info;
            List<Guid> Ids;
            List<PokedexInfoNC> infos = new List<PokedexInfoNC>();
            foreach (var pokemon in pokedex.SelectPokemons)
            {
                Ids = SelectPokemonsQualitiesId(pokemon.Id);
                info = new PokedexInfoNC()
                {
                    Name = pokemon.Name,
                    NumberPo = pokemon.NumberPo,
                    Picture = SpecificPokemonPicture(pokemon.PictureId),
                    TypeName = SelectPokemonsTypeName(Ids),
                    Color = SelectPokemonsTypeColor(Ids)

                };
                infos.Add(info);
            }
            return View(infos);
        }
        // GET: Home/PokemonInfo/Bulbasaur
        public ActionResult PokemonInfo(string Name)
        {
            PrincipalPokedexInfo pokedex = new PrincipalPokedexInfo();
            pokedex.Pokemon = GetOnePokemon(Name);
            PokedexInfoComplete info;
            info = new PokedexInfoComplete()
            {
                NumberPo = pokedex.Pokemon.NumberPo,
                Name = pokedex.Pokemon.Name,
                Description = pokedex.Pokemon.Description,
                Height = float.Parse(pokedex.Pokemon.Height.ToString()),
                Weight = float.Parse(pokedex.Pokemon.Weight.ToString()),
                Picture = SpecificPokemonPicture(pokedex.Pokemon.PictureId),
                CategoryName = GetCategoryName(pokedex.Pokemon.CategoryId),
                HabilityName = GetHabilityName(pokedex.Pokemon.HabilityId),
                TypeFName =SelectPokemonsTypeName(SelectPokemonsQualitiesId(pokedex.Pokemon.Id)),
                FColor = SelectPokemonsTypeColor(SelectPokemonsQualitiesId(pokedex.Pokemon.Id)),
                TypeWName = SelectPokemonsTypeName(SelectPokemonsDebilitiesId(pokedex.Pokemon.Id)),
                WColor = SelectPokemonsTypeColor(SelectPokemonsDebilitiesId(pokedex.Pokemon.Id)),
                FinalRowName = FinalRowName(),
                back = backrowName(pokedex.Pokemon.NumberPo),
                front = frontrowName(pokedex.Pokemon.NumberPo)
            };
            
            return View(info);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //-------------------------------------------
        //Helpers Method Index
        //-------------------------------------------
        List<PokedexModel> GetRecords()
        {
            int n = 0;
            List<PokedexModel> list = new List<PokedexModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("select * from Pokedex order by NumberPo asc", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    PokedexModel Pokedex = new PokedexModel
                    {
                        Id = new Guid(dt.Rows[n]["Id"].ToString()),
                        Seq = Convert.ToInt32(dt.Rows[n]["Seq"].ToString()),
                        NumberPo = dt.Rows[n]["NumberPo"].ToString(),
                        Name = dt.Rows[n]["Name"].ToString(),
                        PictureId = new Guid(dt.Rows[n]["PictureId"].ToString()),
                        CategoryId = new Guid(dt.Rows[n]["CategoryId"].ToString()),
                        Height = float.Parse(dt.Rows[n]["Height"].ToString()),
                        Weight = float.Parse(dt.Rows[n]["Weight"].ToString()),
                        HabilityId = new Guid(dt.Rows[n]["HabilityId"].ToString()),
                        Description = dt.Rows[n]["Description"].ToString(),
                        CreatedDate = Convert.ToDateTime(dt.Rows[n]["CreatedDate"])
                    };
                    n++;
                    list.Add(Pokedex);

                }
            }
            return list;
        }
        // Select pokemon picture
        /*
        List<PicturePokedexModel> SpecificPokemonPicture(IEnumerable<PokedexModel> data)
        {
            List<PicturePokedexModel> list = new List<PicturePokedexModel>();
            foreach (var pokemonrow in data)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connString))
                {
                    SqlCommand sqlCommand = new SqlCommand($"Select * from PicturePokedex where Id = '{pokemonrow.PictureId}'", sqlConnection);
                    sqlConnection.Open();

                    SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    
                    PicturePokedexModel Picture = new PicturePokedexModel
                    {
                        Id = new Guid(dt.Rows[0]["Id"].ToString()),
                        Seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()),
                        Picture = dt.Rows[0]["Picture"].ToString(),
                        CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"])
                    };
                    list.Add(Picture);
                }
            }
            return list;
        }
        */
        string SpecificPokemonPicture(Guid id)
        {
            string picturestring = "";

            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select Picture from PicturePokedex where Id ='{id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                picturestring = dt.Rows[0]["Picture"].ToString();


            }
            return picturestring;

        }
        // Select pokemonsQualities
        List<Guid> SelectPokemonsQualitiesId(Guid Id)
        {
            List<Guid> qualities = new List<Guid>();
            int n = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select TypeId from PokedexTypeQuality where PokedexId = '{Id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    qualities.Add(new Guid(row["TypeId"].ToString()));
                    n++;
                }
            }
            return qualities;

        }
        // Select Pokemon TypeName
        List<string> SelectPokemonsTypeName(List<Guid> ids)
        {
            List<string> TypeName = new List<string>();
            foreach (var id in ids)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connString))
                {
                    SqlCommand sqlCommand = new SqlCommand($"select Name from Type where Id = '{id}'", sqlConnection);
                    sqlConnection.Open();

                    SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        TypeName.Add(row["Name"].ToString());
                    }
                }
            }
            return TypeName;

        }
        // Select Pokemon TypeColor
        List<string> SelectPokemonsTypeColor(List<Guid> ids)
        {
            List<string> TypeColor = new List<string>();
            foreach (var id in ids)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connString))
                {
                    SqlCommand sqlCommand = new SqlCommand($"select Color from Type where Id = '{id}'", sqlConnection);
                    sqlConnection.Open();

                    SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        TypeColor.Add(row["Color"].ToString());
                    }
                }
            }
            return TypeColor;

        }

        //-------------------------------------------
        //Helpers of PokemonInfo
        //-------------------------------------------
        // Get Pokemon of Pokedex Table
        PokedexModel GetOnePokemon(string name)
        {
            PokedexModel Pokedex;
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from Pokedex where Name = '{name}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                Pokedex = new PokedexModel()
                {
                    Id = new Guid(dt.Rows[0]["Id"].ToString()),
                    Seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()),
                    NumberPo = dt.Rows[0]["NumberPo"].ToString(),
                    Name = dt.Rows[0]["Name"].ToString(),
                    PictureId = new Guid(dt.Rows[0]["PictureId"].ToString()),
                    CategoryId = new Guid(dt.Rows[0]["CategoryId"].ToString()),
                    Height = float.Parse(dt.Rows[0]["Height"].ToString()),
                    Weight = float.Parse(dt.Rows[0]["Weight"].ToString()),
                    HabilityId = new Guid(dt.Rows[0]["HabilityId"].ToString()),
                    Description = dt.Rows[0]["Description"].ToString(),
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"])
                };
            }
            return Pokedex;
        }
        // Get Pokemon of Category Table
        // Get Category Name
        string GetCategoryName(Guid id)
        {
            string CategoryName = "";

            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select Name from Category where Id ='{id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                CategoryName = dt.Rows[0]["Name"].ToString();


            }
            return CategoryName;
        }
        //Get Hability Name
        string GetHabilityName(Guid id)
        {
            string HabilityName = "";

            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select Name from Hability where Id ='{id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                HabilityName = dt.Rows[0]["Name"].ToString();


            }
            return HabilityName;
        }
        // Select pokemonsDebilities
        List<Guid> SelectPokemonsDebilitiesId(Guid Id)
        {
            List<Guid> debilities = new List<Guid>();
            int n = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select TypeId from PokedexTypeWeak where PokedexId = '{Id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    debilities.Add(new Guid(row["TypeId"].ToString()));
                    n++;
                }
            }
            return debilities;

        }
        // Get Final Row Name of pokedex Table
        string FinalRowName()
        {
            string FinalRowName = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("select Name from Pokedex where NumberPo = (select count(*) from Pokedex);", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                FinalRowName = dt.Rows[0][0].ToString();
                sqlConnection.Close();
            }
            return FinalRowName;
        }
        string backrowName(string numberPo)
        {
           
            string backRowName = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select Name from Pokedex where NumberPo = {numberPo} - 1", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                try
                {
                    backRowName = dt.Rows[0][0].ToString();
                }
                catch { }
                sqlConnection.Close();
            }
            return backRowName;
        }
        string frontrowName(string numberPo)
        {
            string frontRowName = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select Name from Pokedex where NumberPo = {numberPo} + 1", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                try
                {
                    frontRowName = dt.Rows[0][0].ToString();
                }
                catch { }
                sqlConnection.Close();
            }
            return frontRowName;
        }
    }
}