using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Vacation360.Models.Orm;

namespace Vacation360.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(vaMember hotelMember)
        {
            vaMember loggedinHotel;

            using (Entities db = new Entities())
            {
                loggedinHotel = db.Member.SingleOrDefault(x =>
                   x.Email == hotelMember.Email &&
                   x.Password == hotelMember.Password);

                if (loggedinHotel == null)
                {
                    ModelState.AddModelError("Login Failed", "wrong email or password");
                    return View();
                }
            }

            return RedirectToAction("Index/" + loggedinHotel.ID, "Dashboard");
        }
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(vaMember newmember)
        {


            newmember.LastSeen = DateTime.Now;

            using (Entities db = new Entities())
            {
                db.Member.Add(newmember);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            }


            return RedirectToAction("Index", "Home");
        }

        public JsonResult Hotel()
        {
            Entities db = new Entities();
            
            List<JsonHotel> json = new List<JsonHotel>();
            foreach (vaHotel hotel in db.Hotel.ToList())
            {
                json.Add
                (
                    new JsonHotel()
                    {
                        id = hotel.ID,
                        name = hotel.Name,
                        city = hotel.City.Name,
                    }
                );
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Member()
        {
            Entities db = new Entities();

            List<JsonMember> jsonmember = new List<JsonMember>();
            foreach (vaMember member in db.Member.ToList())
            {
                jsonmember.Add(
                     new JsonMember()
                     {
                         id = member.ID,
                         email = member.Email,
                         password = member.Password
                     }
                 );
            }
           
            return new JsonResult() { Data = jsonmember, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult Photo()
        {
            Entities db = new Entities();
            var results = db.Photo.ToList();
            return new JsonResult() { Data = results, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

     /*   public void PostMember()
        {

            Member member = new Member()
            {
               Email = HttpContext.Current.Session["email"], 
               Password = HttpContext.Current.Session["password"]
            };
            Register(member);
            
        } */
    }

    public class JsonHotel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string url { get; set; }
    }

    public class JsonMember
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}