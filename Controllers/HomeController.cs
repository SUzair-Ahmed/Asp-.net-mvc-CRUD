using againcrud.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace againcrud.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db = new UsersContext();
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            string filename = Path.GetFileName(student.File.FileName);
            string _filename = DateTime.Now.ToString("hhmmsfff") + filename;
            string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
            student.image = "~/Images/" + _filename;
            db.Students.Add(student);
            if (db.SaveChanges() > 0)
            {
                student.File.SaveAs(path);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = db.Students.Single(x => x.Id == id);
            Session["imgPath"] = data.image;
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int id, Student student)
        {
            if (student.File != null)
            {

                string filename = Path.GetFileName(student.File.FileName);
                string _filename = DateTime.Now.ToString("hhmmsfff") + filename;
                string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                student.image = "~/Images/" + _filename;
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                string oldImgPath = Request.MapPath(Session["imgPath"].ToString());


                if (db.SaveChanges() > 0)
                {
                    student.File.SaveAs(path);
                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);

                    }

                }
                return RedirectToAction("Index");

            }
            else 
            {
                // Logic to handle when no new image is uploaded
                student.image = Session["imgPath"].ToString();
                Student data = db.Students.Single(y => y.Id == id);

                data.Name = student.Name;
                data.Age = student.Age;
                data.Gender = student.Gender; // Ensure you update other fields as needed

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(student); 
       }


        public ActionResult Delete(int id)
        {
            var data = db.Students.Single(x => x.Id == id);
            string currentImage = Request.MapPath(data.image);
            db.Students.Remove(data);
            if (db.SaveChanges() > 0)
            {

                if ( System.IO.File.Exists(currentImage))
                {
                 System.IO.File.Delete(currentImage);
                }
            }
            return RedirectToAction("Index");
        
        }
    }
}