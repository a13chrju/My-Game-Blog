using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
            bloggs blogg = new bloggs();

            con.Open();

            MySqlCommand fetchdata2 = new MySqlCommand("SELECT * FROM post order by id desc limit 0,4", con);
            MySqlDataReader r2 = fetchdata2.ExecuteReader();

            List<blogg> allbloggar = new List<blogg>();

            //connect2


            List<material> allmaterials;

            while (r2.Read())
            {
                allmaterials = new List<material>();

                blogg post = new blogg();
                post.titel = r2["titel"].ToString();
                post.text = r2["text"].ToString();
                post.thumbnail = r2["thumbnail"].ToString();
                post.episode = Convert.ToInt32(r2["episode"]);
                post.category = Convert.ToInt32(r2["category"]); ;
                post.index = Convert.ToInt32(r2["id"]);
                post.datum = r2["datum"].ToString();
                post.video_url = r2["video_url"].ToString();

                MySqlConnection con2 = new MySqlConnection();
                con2.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
                con2.Open();
                MySqlCommand fetchdata3 = new MySqlCommand("SELECT har_material.post_id,materials.imageurl,materials.type,materials.index FROM materials,har_material,post where har_material.material_id = materials.index and har_material.post_id = post.id and har_material.post_id =" + Convert.ToInt32(r2["id"]), con2);
                MySqlDataReader r3 = fetchdata3.ExecuteReader();


                while (r3.Read())
                {
                    material material = new material();
                    material.index = Convert.ToInt32(r3["index"]);
                    material.postid = Convert.ToInt32(r3["post_id"]);
                    material.imageurl = r3["imageurl"].ToString();
                    material.type = Convert.ToInt32(r3["type"]);
                    allmaterials.Add(material);
                }



                post.materials = allmaterials;
                allbloggar.Add(post);
                con2.Close();
            }


            blogg.blogg = allbloggar;

            //fetch all meterials

            con.Close();


            //fetch pagecount

            MySqlConnection con4 = new MySqlConnection();
            con4.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
            MySqlCommand fetchdata4 = new MySqlCommand("SELECT count(*) from post", con4);
            con4.Open();
            MySqlDataReader r4 = fetchdata4.ExecuteReader();
            int sizepages = 3;
            while (r4.Read())
            {

                sizepages = Convert.ToInt32(r4["count(*)"]);

            }

            Pager pagenation2 = new Pager(sizepages, 1, 2);
            blogg.pagenation = pagenation2;
            con4.Close();



            return View(blogg);
        }

        [Route("Home/Materials/{id?}")]
        public ActionResult Materials(int? id)
        {
            DownloadMaterials model = new DownloadMaterials();


            MySqlConnection con4 = new MySqlConnection();
            con4.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
            con4.Open();
            MySqlCommand fetchdata3 = new MySqlCommand("SELECT * from materials", con4);
            MySqlDataReader r3 = fetchdata3.ExecuteReader();
            List<material> allmaterials = new List<material>();

            while (r3.Read())
            {
                material material = new material();
                material.index = Convert.ToInt32(r3["index"]);
                material.description = r3["description"].ToString();
                material.BlenderFile = r3["BlenderFile"].ToString();
                material.imageurl = r3["imageurl"].ToString();
                material.type = Convert.ToInt32(r3["type"]);
                allmaterials.Add(material);
            }
            model.materials = allmaterials;
            con4.Close();


            var selectedmaterial = new material();

            if (id != null)
            {
                MySqlConnection con5 = new MySqlConnection();
                con5.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
                con5.Open();
                MySqlCommand fetchdata5 = new MySqlCommand("SELECT * from materials where materials.index =" + id, con5);
                MySqlDataReader r5 = fetchdata5.ExecuteReader();

                while (r5.Read())
                {
                    material material = new material();
                    material.index = Convert.ToInt32(r5["index"]);
                    material.description = r5["description"].ToString();
                    material.BlenderFile = r5["BlenderFile"].ToString();
                    material.imageurl = r5["imageurl"].ToString();
                    material.type = Convert.ToInt32(r5["type"]);
                    selectedmaterial = material;
                }
                model.selectedmaterial = selectedmaterial;
                con5.Close();
            }

            return View(model);
        }

        public ActionResult FreeContent(int materialid = 1, int postid = 0)
        {
            ViewBag.materialid = materialid;
            ViewBag.postid = postid;
            var model = new matpost();
            //all materials
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
            MySqlCommand fetchdata = new MySqlCommand("SELECT * from materials ORDER BY RAND() LIMIT 4", con);
            con.Open();
            MySqlDataReader r = fetchdata.ExecuteReader();

            List<material> materials = new List<material>();

            while (r.Read())
            {
                material material = new material();
                material.index = Convert.ToInt32(r["index"]);
                material.imageurl = r["imageurl"].ToString();
                material.description = r["description"].ToString();
                material.type = Convert.ToInt32(r["type"]);
                material.BlenderFile = r["BlenderFile"].ToString();
                materials.Add(material);
            }


            model.materials = materials;
            con.Close();


            MySqlConnection con2 = new MySqlConnection();
            con2.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
            MySqlCommand fetchdata2 = new MySqlCommand("SELECT * FROM post ORDER BY RAND() LIMIT 6", con2);

            con2.Open();

            MySqlDataReader r2 = fetchdata2.ExecuteReader();

            List<blogg> posts = new List<blogg>();

            //connect2


            List<material> allmaterials;

            while (r2.Read())
            {
                allmaterials = new List<material>();

                blogg post = new blogg();
                post.titel = r2["titel"].ToString();
                post.datum = r2["datum"].ToString();
                post.text = r2["text"].ToString();
                post.episode = Convert.ToInt32(r2["episode"]);
                post.thumbnail = r2["thumbnail"].ToString();
                post.category = Convert.ToInt32(r2["category"]);
                post.index = Convert.ToInt32(r2["id"]);
                post.video_url = r2["video_url"].ToString();


                MySqlConnection con4 = new MySqlConnection();
                con4.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
                con4.Open();
                MySqlCommand fetchdata3 = new MySqlCommand("SELECT har_material.post_id,materials.imageurl,materials.BlenderFile,materials.type,materials.index FROM materials,har_material,post where har_material.material_id = materials.index and har_material.post_id = post.id and har_material.post_id =" + Convert.ToInt32(r2["id"]), con4);
                MySqlDataReader r3 = fetchdata3.ExecuteReader();


                while (r3.Read())
                {
                    material material = new material();
                    material.index = Convert.ToInt32(r3["index"]);
                    material.postid = Convert.ToInt32(r3["post_id"]);
                    material.BlenderFile = r3["BlenderFile"].ToString();
                    material.imageurl = r3["imageurl"].ToString();
                    material.type = Convert.ToInt32(r3["type"]);
                    allmaterials.Add(material);
                }
                post.materials = allmaterials;

                if (allmaterials.Count() != 0)
                {
                    model.selectedmaterial = allmaterials.First();
                }
            

                posts.Add(post);
                con4.Close();
            }
            model.posts = posts;



            con2.Close();

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult pinpost(int id = 1)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
            MySqlCommand fetchdata = new MySqlCommand("SELECT * from post where id = " + id + ";", con);
            con.Open();
            MySqlDataReader r = fetchdata.ExecuteReader();

            blogg post = new blogg();

            while (r.Read())
            {
                post.index = Convert.ToInt32(r["id"]);
                post.titel = r["titel"].ToString();
                post.text = r["text"].ToString();
                post.datum = r["datum"].ToString();
                post.video_url = r["video_url"].ToString();
            }



            con.Close();

            return Json(post, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getmatcategory(int id = 1)
        {

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
            MySqlCommand fetchdata = new MySqlCommand("SELECT * from materials where type = " + id, con);
            con.Open();
            MySqlDataReader r = fetchdata.ExecuteReader();

            List<material> materials = new List<material>();

            while (r.Read())
            {
                material material = new material();
                material.index = Convert.ToInt32(r["index"]);
                material.imageurl = r["imageurl"].ToString();
                material.description = r["description"].ToString();
                material.type = Convert.ToInt32(r["type"]);
                materials.Add(material);
            }


            
            con.Close();



            return Json(materials, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetBlenderFile(string FileUrl)
        {
            if (!String.IsNullOrEmpty(FileUrl))
            {
                using (WebClient wc = new WebClient())
                {
                      var byteArr = wc.DownloadData(FileUrl);

                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = FileUrl,
                        Inline = true,
                    };

                    Response.AppendHeader("Content-Disposition", cd.ToString());

                    return File(byteArr, "application/octet-stream");
                   
                 
                }

            }
            return Content("No file name provided");
        }

        public ActionResult page(int id)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
            MySqlCommand fetchdata = new MySqlCommand("SELECT har_tagg.post_id,Tagg.namn FROM Tagg,har_tagg where har_tagg.tagg_id = Tagg.index", con);
            con.Open();
            MySqlDataReader r = fetchdata.ExecuteReader();
            bloggs blogg = new bloggs();

            List<category> skategorier = new List<category>();

            while (r.Read())
            {
                category Category = new category();
                Category.name = r["namn"].ToString();
                Category.index = Convert.ToInt32(r["post_id"]);
                skategorier.Add(Category);
            }

            blogg.kategorier = skategorier;

            con.Close();
            con.Open();
            int pagenation = (id - 1) * 4;

            MySqlCommand fetchdata2 = new MySqlCommand("SELECT * FROM post order by id desc limit "+pagenation +","+ 4, con);
            MySqlDataReader r2 = fetchdata2.ExecuteReader();

            List<blogg> allbloggar = new List<blogg>();

            //connect2


            List<material> allmaterials;

            while (r2.Read())
            {
                allmaterials = new List<material>();

                blogg post = new blogg();
                post.titel = r2["titel"].ToString();
                post.text = r2["text"].ToString();
                post.episode = Convert.ToInt32(r2["episode"]);
                post.category = Convert.ToInt32(r2["category"]);
                post.index = Convert.ToInt32(r2["id"]);
                post.datum = r2["datum"].ToString();
                post.video_url = r2["video_url"].ToString();

                MySqlConnection con2 = new MySqlConnection();
                con2.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
                con2.Open();
                MySqlCommand fetchdata3 = new MySqlCommand("SELECT har_material.post_id,materials.imageurl,materials.type,materials.index FROM materials,har_material,post where har_material.material_id = materials.index and har_material.post_id = post.id and har_material.post_id =" + Convert.ToInt32(r2["id"]), con2);
                MySqlDataReader r3 = fetchdata3.ExecuteReader();


                while (r3.Read())
                {
                    material material = new material();
                    material.index = Convert.ToInt32(r3["index"]);
                    material.postid = Convert.ToInt32(r3["post_id"]);
                    material.imageurl = r3["imageurl"].ToString();
                    material.type = Convert.ToInt32(r3["type"]);
                    allmaterials.Add(material);
                }



                post.materials = allmaterials;
                allbloggar.Add(post);
                con2.Close();
            }


            blogg.blogg = allbloggar;

            //fetch all meterials

            con.Close();

            //fetch pagecount

            MySqlConnection con4 = new MySqlConnection();
            con4.ConnectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
            MySqlCommand fetchdata4 = new MySqlCommand("SELECT count(*) from post", con4);
            con4.Open();
            MySqlDataReader r4 = fetchdata4.ExecuteReader();
            int sizepages= 3;
            while (r4.Read())
            {

                sizepages = Convert.ToInt32(r4["count(*)"]);
            
            }

            Pager pagenation2 = new Pager(sizepages,id,2);
            blogg.pagenation = pagenation2;
            con4.Close();



            return View("index", blogg);
        }


        ActionResult SelfPost(int id = 1)
        {

            return View();
        }
    }
}