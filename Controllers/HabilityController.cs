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
    public class HabilityController : Controller
    {
        // GET: Hability
        readonly string connString = "Server=DESKTOP-453205K;Database=Pokemon;Trusted_Connection=True;";

        public ActionResult Index()
        {
            List<PokedexHability> list = GetRecords();
            return View(list);
        }

        // GET: Hability/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hability/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PokedexHability hability)
        {
            try
            {
                // TODO: Add insert logic here
                Insert(hability);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hability/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PokedexHability hability = GetRow(id);
            return View(hability);
        }

        // POST: Hability/Edit/5
        [HttpPost]
        public ActionResult Edit(PokedexHability hability)
        {
            try
            {
                // TODO: Add update logic here
                Update(hability);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hability/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PokedexHability hability = GetRow(id);
            return View(hability);
        }

        // POST: Hability/Delete/5
        [HttpPost]
        public ActionResult Delete(PokedexHability hability)
        {
            try
            {
                // TODO: Add delete logic here
                DeleteHability(hability.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Helpers Method Crud
        // Update Hability Pokemon
        void Update(PokedexHability hability)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Update Hability Set Name = '{hability.Name}' where Id = '{hability.Id}'", sqlConnection);
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

        // Delete Hability Pokemon
        void DeleteHability(Guid id)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Delete From Hability where Id = '{id}'", sqlConnection);
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
        //Insert Hability Pokemon
        void Insert(PokedexHability Hability)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Insert into Hability (Name) Values ('{Hability.Name}')", sqlConnection);
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
        List<PokedexHability> GetRecords()
        {
            int n = 0;
            List<PokedexHability> list = new List<PokedexHability>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("select * from Hability", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    PokedexHability Hability = new PokedexHability
                    {
                        Id = new Guid(row["Id"].ToString()),
                        Seq = Convert.ToInt32(row["Seq"].ToString()),
                        Name = row["Name"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    };
                    n++;
                    list.Add(Hability);

                }
            }
            return list;
        }
        // GetRow of Hability
        PokedexHability GetRow(Guid? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from Hability where Id = '{id}'", sqlConnection);
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