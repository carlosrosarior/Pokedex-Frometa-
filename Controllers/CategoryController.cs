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
    public class CategoryController : Controller
    {
        // GET: Category
        readonly string connString = "Server=DESKTOP-453205K;Database=Pokemon;Trusted_Connection=True;";

        public ActionResult Index()
        {
            List<PokedexCategory> list = GetRecords();
            return View(list);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PokedexCategory category)
        {
            try
            {
                // TODO: Add insert logic here
                Insert(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PokedexHability hability = GetRow(id);
            return View(hability);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(PokedexCategory Category)
        {
            try
            {
                // TODO: Add update logic here
                Update(Category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(Guid id)
        {
            if(id == null) { return RedirectToAction("Index"); };
            PokedexHability hability = GetRow(id);
            return View(hability);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(PokedexCategory category)
        {
            try
            {
                // TODO: Add delete logic here
                DeleteCategory(category.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Helpers Method Crud
        // Update Category Pokemon
        void Update(PokedexCategory Category)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Update Category Set Name = '{Category.Name}' where Id = '{Category.Id}'", sqlConnection);
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

        // Delete Category Pokemon
        void DeleteCategory(Guid id)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Delete From Category where Id = '{id}'", sqlConnection);
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
        //Insert Category Pokemon
        void Insert(PokedexCategory Category)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Insert into Category (Name) Values ('{Category.Name}')", sqlConnection);
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
        List<PokedexCategory> GetRecords()
        {
            int n = 0;
            List< PokedexCategory> list = new List<PokedexCategory>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("select * from Category", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    PokedexCategory Category  = new PokedexCategory
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
        // GetRow of Category
        PokedexHability GetRow(Guid? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from Category where Id = '{id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                PokedexHability hability = new PokedexHability()
                {
                    Id = new Guid(dt.Rows[0]["Id"].ToString()),
                    Seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()),
                    Name = dt.Rows[0]["Name"].ToString(),
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"])
                };
                return hability;
            }
        }
    }
}