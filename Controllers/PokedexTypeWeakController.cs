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
    public class PokedexTypeWeakController : Controller
    {
        // GET: PokedexTypeQuality
        readonly string connString = "Server=DESKTOP-453205K;Database=Pokemon;Trusted_Connection=True;";
        public ActionResult Index()
        {
            var PokedexTypeQuality = GetRecords();
            return View(PokedexTypeQuality);
        }

        // GET: PokedexTypeQuality/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PokedexTypeQuality/Create
        public ActionResult Create()
        {
            PokedexAll pokedex = new PokedexAll();
            List<PokedexModel> AllPokemon = SelectAllPokemon();
            List<PokedexTypeModel> AllType = SelectAllType();
            pokedex.SelectType = AllType;
            pokedex.SelectPokemons = AllPokemon;

            return View(pokedex);
        }

        // POST: PokedexTypeQuality/Create
        [HttpPost]
        public ActionResult Create(PokedexTypeWeak TypeWeak)
        {
            try
            {
                // TODO: Add insert logic here
                Insert(TypeWeak);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PokedexTypeQuality/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PokedexAll pokedex = new PokedexAll();
            List<PokedexModel> AllPokemon = SelectAllPokemon();
            List<PokedexTypeModel> AllType = SelectAllType();
            pokedex.SelectType = AllType;
            pokedex.SelectPokemons = AllPokemon;
            pokedex.SelectTypeWeak = GetRow(id);

            return View(pokedex);
        }

        // POST: PokedexTypeQuality/Edit/5
        [HttpPost]
        public ActionResult Edit(PokedexTypeWeak TypeWeak)
        {
            try
            {
                // TODO: Add update logic here
                Update(TypeWeak);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PokedexTypeQuality/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PokedexTypeWeak TypeWeak = GetRow(id);
            return View(TypeWeak);
        }

        // POST: PokedexTypeQuality/Delete/5
        [HttpPost]
        public ActionResult Delete(PokedexTypeWeak pokedexTypeWeak)
        {
            try
            {
                // TODO: Add delete logic here
                DeleteWeak(pokedexTypeWeak.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //Helpers Method Crud
        // Update TypeQuality Pokemon
        void Update(PokedexTypeWeak Weak)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Update PokedexTypeWeak Set PokedexId = '{Weak.PokedexId}', TypeId = '{Weak.TypeId}' where Id = '{Weak.Id}'", sqlConnection);
                sqlConnection.Open();
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    message = e.ToString();
                }

                sqlConnection.Close();
            }
            ViewBag.Message = message;
        }

        // Delete TypeQuality Pokemon
        void DeleteWeak(Guid id)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Delete From PokedexTypeWeak where Id = '{id}'", sqlConnection);
                sqlConnection.Open();
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    message = e.ToString();
                }

                sqlConnection.Close();
            }
            ViewBag.Message = message;
        }
        //Insert TypeQuality Pokemon
        void Insert(PokedexTypeWeak Weak)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Insert into PokedexTypeWeak (PokedexId,TypeId) Values ('{Weak.PokedexId}','{Weak.TypeId}')", sqlConnection);
                sqlConnection.Open();
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    message = e.ToString();
                }

                sqlConnection.Close();
            }
            ViewBag.Message = message;
        }
        //Select All Data 
        List<PokedexTypeWeak> GetRecords()
        {
            int n = 0;
            List<PokedexTypeWeak> list = new List<PokedexTypeWeak>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("select * from PokedexTypeWeak", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    PokedexTypeWeak Weak = new PokedexTypeWeak
                    {
                        Id = new Guid(row["Id"].ToString()),
                        Seq = Convert.ToInt32(row["Seq"].ToString()),
                        PokedexId = new Guid(row["Pokedexid"].ToString()),
                        TypeId = new Guid(row["TypeId"].ToString())
                    };
                    n++;
                    list.Add(Weak);

                }
            }
            return list;
        }
        // GetRow of TypeWeak
        PokedexTypeWeak GetRow(Guid? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from PokedexTypeWeak where Id = '{id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                PokedexTypeWeak Weak = new PokedexTypeWeak()
                { 
                    Id = new Guid(dt.Rows[0]["Id"].ToString()),
                    Seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()),
                    PokedexId = new Guid(dt.Rows[0]["Pokedexid"].ToString()),
                    TypeId = new Guid(dt.Rows[0]["TypeId"].ToString())
                };
                return Weak;
            }
        }

        //Select All Pokemon
        List<PokedexModel> SelectAllPokemon()
        {
            int n = 0;
            List<PokedexModel> list = new List<PokedexModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("select * from Pokedex", sqlConnection);
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
                        Description = dt.Rows[0]["Description"].ToString(),
                        CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"])
                    };
                    n++;
                    list.Add(Pokedex);

                }
            }
            return list;
        }
        //Select All
        List<PokedexTypeModel> SelectAllType()
        {
            int n = 0;
            List<PokedexTypeModel> list = new List<PokedexTypeModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("select * from Type", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    PokedexTypeModel Type = new PokedexTypeModel
                    {
                        Id = new Guid(row["Id"].ToString()),
                        Seq = Convert.ToInt32(row["Seq"].ToString()),
                        Name = row["Name"].ToString(),
                        Color = row["Color"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    };
                    n++;
                    list.Add(Type);

                }
            }
            return list;
        }
    }
}
