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
    public class PokedexController : Controller
    {
        readonly string connString = "Server=DESKTOP-453205K;Database=Pokemon;Trusted_Connection=True;";
        // GET: Pokedex
        public ActionResult Index()
        {
            List<PokedexModel> list = GetRecords();
            return View(list);
        }

        // GET: Pokedex/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pokedex/Create
        public ActionResult Create()
        {
            PokedexAll pokedex = new PokedexAll();
            List<PicturePokedexModel> AllPicture = SelectAllPicture();
            List<PokedexHability> AllHabilities = SelectAllHabilities();
            List<PokedexCategory> Allcategories = SelectAllCategories();
            pokedex.SelectPicture = AllPicture;
            pokedex.SelectHability = AllHabilities;
            pokedex.SelectCategory = Allcategories;
            return View(pokedex);
        }

        // POST: Pokedex/Create
        [HttpPost]
        public ActionResult Create(PokedexModel Pokedex)
        {
            try
            {
                // TODO: Add insert logic here
                Insert(Pokedex);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pokedex/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PokedexAll pokedex = new PokedexAll();
            List<PicturePokedexModel> AllPicture = SelectAllPicture();
            List<PokedexHability> AllHabilities = SelectAllHabilities();
            List<PokedexCategory> Allcategories = SelectAllCategories();
            PokedexModel OnePokemon = GetRow(id);
            pokedex.SelectPicture = AllPicture;
            pokedex.SelectHability = AllHabilities;
            pokedex.SelectCategory = Allcategories;
            pokedex.SelectPokemon = OnePokemon;
            return View(pokedex);
        }

        // POST: Pokedex/Edit/5
        [HttpPost]
        public ActionResult Edit(PokedexModel Pokedex)
        {
            try
            {
                // TODO: Add update logic here
                Update(Pokedex);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pokedex/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PokedexModel Pokedex = GetRow(id);
            return View(Pokedex);
        }

        // POST: Pokedex/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                DeletePokedex(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Helpers Method Crud
        //Select all pokemon picture
        List<PicturePokedexModel> SelectAllPicture()
        {
            int n = 0;
            List<PicturePokedexModel> list = new List<PicturePokedexModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("Select * from PicturePokedex", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    PicturePokedexModel Picture = new PicturePokedexModel
                    {
                        Id = new Guid(row["Id"].ToString()),
                        Seq = Convert.ToInt32(row["Seq"].ToString()),
                        Picture = row["Picture"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    };
                    n++;
                    list.Add(Picture);

                }
            }
            return list;
        }
       //Select all pokemon habilities
        List<PokedexHability> SelectAllHabilities()
        {
            int n = 0;
            List<PokedexHability> list = new List<PokedexHability>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("Select * from Hability", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    PokedexHability hability = new PokedexHability
                    {
                        Id = new Guid(row["Id"].ToString()),
                        Seq = Convert.ToInt32(row["Seq"].ToString()),
                        Name = row["Name"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    };
                    n++;
                    list.Add(hability);

                }
            }
            return list;
        }
        //Select all pokemon Category
        List<PokedexCategory> SelectAllCategories()
        {
            int n = 0;
            List<PokedexCategory> list = new List<PokedexCategory>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("Select * from Category", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    PokedexCategory Category = new PokedexCategory
                    {
                        Id = new Guid(row["Id"].ToString()),
                        Seq = Convert.ToInt32(row["Seq"].ToString()),
                        Name = row["Name"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    };
                    n++;
                    list.Add(Category);

                }
            }
            return list;
        }
        // Update Pokemon
        void Update(PokedexModel Pokedex)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Update Pokedex Set NumberPo='{Pokedex.NumberPo}', Name = '{Pokedex.Name}', PictureId = '{Pokedex.PictureId}', CategoryId = '{Pokedex.CategoryId}', Height = '{Pokedex.Height}', Weight = '{Pokedex.Weight}', HabilityId ='{Pokedex.HabilityId}', Description = '{Pokedex.Description}' where Id = '{Pokedex.Id}'", sqlConnection);
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

        // Delete Pokemon
        void DeletePokedex(Guid id)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Delete From Pokedex where Id = '{id}'", sqlConnection);
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
        //Insert Pokemon
        void Insert(PokedexModel Pokedex)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Insert into Pokedex (NumberPo, Name, PictureId, CategoryId, Height, Weight, HabilityId, Description) Values ('{Pokedex.NumberPo}','{Pokedex.Name}','{Pokedex.PictureId}','{Pokedex.CategoryId}','{Pokedex.Height}','{Pokedex.Weight}','{Pokedex.HabilityId}','{Pokedex.Description}')", sqlConnection);
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
        List<PokedexModel> GetRecords()
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
                        Description = dt.Rows[n]["Description"].ToString(),
                        CreatedDate = Convert.ToDateTime(dt.Rows[n]["CreatedDate"])
                    };
                    n++;
                    list.Add(Pokedex);

                }
            }
            return list;
        }
        // GetRow of the Pokemon
        PokedexModel GetRow(Guid? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from Pokedex where Id = '{id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                PokedexModel Pokedex = new PokedexModel()
                {
                    Id = new Guid(dt.Rows[0]["Id"].ToString()),
                    Seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()),
                    NumberPo = dt.Rows[0]["NumberPo"].ToString(),
                    Name = dt.Rows[0]["Name"].ToString(),
                    PictureId = new Guid(dt.Rows[0]["PictureId"].ToString()),
                    CategoryId = new Guid(dt.Rows[0]["CategoryId"].ToString()),
                    Height = Convert.ToInt32(dt.Rows[0]["Height"]),
                    Weight = Convert.ToInt32(dt.Rows[0]["Weight"]),
                    HabilityId = new Guid(dt.Rows[0]["HabilityId"].ToString()),
                    Description = dt.Rows[0]["Description"].ToString(),
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"])
                };
                return Pokedex;
            }
        }
    }
}
