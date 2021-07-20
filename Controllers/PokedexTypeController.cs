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
    public class PokedexTypeController : Controller
    {
        readonly string connString = "Server=DESKTOP-453205K;Database=Pokemon;Trusted_Connection=True;";
        // GET: PokedexType
        
        public ActionResult Index()
        {
            List<PokedexTypeModel> list = GetRecords();
            return View(list);
        }

        // GET: PokedexType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PokedexType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PokedexTypeModel Type)
        {
            try
            {
                // TODO: Add insert logic here
                Insert(Type);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PokedexType/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PokedexTypeModel Type = GetRow(id);
            return View(Type);
        }

        // POST: PokedexType/Edit/5
        [HttpPost]
        public ActionResult Edit(PokedexTypeModel Type)
        {
            try
            {
                // TODO: Add update logic here
                Update(Type);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PokedexType/Delete/5
        public ActionResult Delete(Guid id)
        {
            PokedexTypeModel Type = GetRow(id);
            return View(Type);
        }

        // POST: PokedexType/Delete/5
        [HttpPost]
        public ActionResult Delete(PokedexTypeModel Type)
        {
            try
            {
                // TODO: Add delete logic here
                DeleteType(Type.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Helpers Method Crud
        // Update Type Quality or Type Weak
        void Update(PokedexTypeModel Type)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Update Type Set Name = '{Type.Name}', Color = '{Type.Color}'  where Id = '{Type.Id}'", sqlConnection);
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

        // Delete Type Quality or Type Weak
        void DeleteType(Guid id)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Delete From Type where Id = '{id}'", sqlConnection);
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
        //Insert Type Quality or Type Weak
        void Insert(PokedexTypeModel Type)
        {
            var message = "";
            using(SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Insert into Type (Name, Color) Values ('{Type.Name}','{Type.Color}')", sqlConnection);
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
        List<PokedexTypeModel> GetRecords()
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
        // GetRow Type Quality or Type Weak
        PokedexTypeModel GetRow(Guid? id)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from Type where Id = '{id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                PokedexTypeModel Type = new PokedexTypeModel()
                {
                    Id = new Guid(dt.Rows[0]["Id"].ToString()),
                    Seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()),
                    Name = dt.Rows[0]["Name"].ToString(),
                    Color = dt.Rows[0]["Color"].ToString(),
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"])
                };
                return Type;
            }
        }
    }
}
