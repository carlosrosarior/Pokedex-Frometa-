using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Pokedex.Models;
using System.IO;

namespace Pokedex.Controllers
{
    public class PicturePokedexController : Controller
    {
        readonly string connString = "Server=DESKTOP-453205K;Database=Pokemon;Trusted_Connection=True;";
        // GET: PicturePokedex
        public ActionResult Index()
        {
            List<PicturePokedexModel> list = GetRecords();
            return View(list);
        }

        // GET: PicturePokedex/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PicturePokedex/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PicturePokedexModel Picture)
        {
            try
            {
                // TODO: Add insert logic here
                if(Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if(file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("/Public/PicturePokemon/"), fileName);
                        file.SaveAs(path);
                        Insert(fileName);
                    }
                }
                else
                {
                    ViewBag.Message = "No se seleccionó la imagen";
                }
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PicturePokedex/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PicturePokedexModel Picture = GetRow(id);
            return View(Picture);
        }

        // POST: PicturePokedex/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid? id)
        {
            try
            {
                // TODO: Add update logic here
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("/Public/PicturePokemon/"), fileName);
                        file.SaveAs(path);
                        Update(fileName, id);
                        
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PicturePokedex/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null) { return RedirectToAction("Index"); };
            PicturePokedexModel Picture = GetRow(id);
            return View(Picture);
        }

        // POST: PicturePokedex/Delete/5
        [HttpPost]
        public ActionResult Delete(PicturePokedexModel picture)
        {
            try
            {
                // TODO: Add delete logic here
                DeletePicture(picture.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Helpers Method Crud
        // Update Picture Pokemon
        void Update(string picture, Guid? id)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Update PicturePokedex Set Picture = '{picture}' where Id = '{id}'", sqlConnection);
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

        // Delete Picture Pokemon
        void DeletePicture(Guid id)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Delete From PicturePokedex where Id = '{id}'", sqlConnection);
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
        //Insert Picture Pokemon
        void Insert(string picture)
        {
            var message = "";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"Insert into PicturePokedex (Picture) Values ('{picture}')", sqlConnection);
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
        List<PicturePokedexModel> GetRecords()
        {
            int n = 0;
            List<PicturePokedexModel> list = new List<PicturePokedexModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand("select * from PicturePokedex", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    PicturePokedexModel Type = new PicturePokedexModel
                    {
                        Id = new Guid(row["Id"].ToString()),
                        Seq = Convert.ToInt32(row["Seq"].ToString()),
                        Picture = row["Picture"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    };
                    n++;
                    list.Add(Type);

                }
            }
            return list;
        }
        // GetRow Picture Pokemon - file
        PicturePokedexModel GetRowfile(string file)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from PicturePokedex where Picture = '{file}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                PicturePokedexModel Type = new PicturePokedexModel()
                {
                    Id = new Guid(dt.Rows[0]["Id"].ToString()),
                    Seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()),
                    Picture = dt.Rows[0]["Picture"].ToString(),
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"])
                };
                return Type;
            }
        }
        // GetRow Picture Pokemon
        PicturePokedexModel GetRow(Guid id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCommand = new SqlCommand($"select * from PicturePokedex where id = '{id}'", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                PicturePokedexModel Picture = new PicturePokedexModel()
                {
                    Id = new Guid(dt.Rows[0]["Id"].ToString()),
                    Seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()),
                    Picture = dt.Rows[0]["Picture"].ToString(),
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"])
                };
                return Picture;
            }
        }
    }
}