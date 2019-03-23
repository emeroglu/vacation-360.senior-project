using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Vacation360.Models;
using Vacation360.Models.Orm;
using Vacation360.Models.Service.Upload;


namespace Vacation360.Controllers
{
    public class DashboardController : Controller
    {


        public ActionResult Index(int id)
        {

            ViewBag.MemberID = id;

            return View();

        }

        public ActionResult ListHotels(int id)
        {
            ViewBag.MemberID = id;
            Entities db = new Entities();

            List<vaHotel> hotels = db.Hotel.Where(x => x.Member.ID == id).ToList();
            return View(hotels);

        }

        [HttpPost]
        public ActionResult ListHotels()
        {
            using (Entities db = new Entities())
            {
                List<vaHotel> hotels = db.Hotel.ToList();
                return View(hotels);
            }
        }
        public ActionResult Add(int id)
        {
            ViewBag.MemberID = id;
            return View();
        }

        [HttpPost]
        public ActionResult Add(int id, vaHotel hotel)
        {
            ViewBag.MemberID = id;
            Entities db = new Entities();

            if (!string.IsNullOrEmpty(hotel.Name))
            {
                if (Request.Files.Count != 0)
                {
                    hotel.Photos = new List<vaPhoto>();

                    int index = 0;
                    string key = "",title = "";

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if (key != Request.Files.GetKey(i))
                            index = 0;

                        key = Request.Files.GetKey(i);                        

                        index++;

                        if (key == "roomPhotos")
                            title = "Room Photo " + index;
                        else if (key == "lobbyPhotos")
                            title = "Lobby Photo " + index;
                        else if (key == "outdoorPhotos")
                            title = "Outdoor Photo " + index;

                        ImageUpload upload = new ImageUpload();
                        string photoUrl = upload.Upload(Request.Files[i], "/Photos/");

                        vaPhoto uploaded = new vaPhoto(photoUrl);
                        uploaded.Title = title;
                        uploaded.RawUrl = "/Photos/" + photoUrl;
                                                                       
                        hotel.Photos.Add(uploaded);
                    }
                }

                hotel.Member = db.Member.FirstOrDefault(m => m.ID == id);

                db.Hotel.Add(hotel);
                db.SaveChanges();

            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.MemberID = id;

            Entities db = new Entities();
            // Member member = db.Member.Find(id);
            vaHotel hotel = db.Hotel.FirstOrDefault(h => h.ID == id);

            return View(hotel);
        }

        [HttpPost]
        public ActionResult Edit(int id, vaHotel model)
        {
            ViewBag.MemberID = id;
            Entities db = new Entities();

            vaHotel hotel = db.Hotel.FirstOrDefault(h => h.ID == id);

            vaMember member = db.Member.FirstOrDefault(x => x.ID == hotel.Member.ID);

            if (Request.Files.Count != 0)
            {
                hotel.Photos = new List<vaPhoto>();

                int index = 0;
                string key = "", title = "";

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if (key != Request.Files.GetKey(i))
                        index = 0;

                    key = Request.Files.GetKey(i);

                    index++;

                    if (key == "roomPhotos")
                        title = "Room Photo " + index;
                    else if (key == "lobbyPhotos")
                        title = "Lobby Photo " + index;
                    else if (key == "outdoorPhotos")
                        title = "Outdoor Photo " + index;

                    ImageUpload upload = new ImageUpload();
                    string photoUrl = upload.Upload(Request.Files[i], "/Photos/");

                    vaPhoto uploaded = new vaPhoto(photoUrl);
                    uploaded.Title = title;
                    uploaded.RawUrl = "/Photos/" + photoUrl;

                    hotel.Photos.Add(uploaded);
                }
            }
                

               /* if (Request.Files.Count != 0)
                {
                    ImageUpload uploader = new ImageUpload();
                    string photoUrl = uploader.Upload(Request.Files[0], "/Photos/");

                    vaPhoto uploaded = new vaPhoto(photoUrl);
                    hotel.Photos.Add(uploaded);
                } */
                hotel.Name = model.Name;
                hotel.PhoneNumber = model.PhoneNumber;
                hotel.Email = model.Email;
                hotel.Address = model.Address;
                hotel.Information = model.Information;
                db.SaveChanges();

                //return RedirectToAction("Index/" + member.ID);
                return View(hotel);

        }



        /*  public JsonResult Publish(int id)
            {
                Entities db = new Entities();
                Hotel hoteltobepublished = db.Hotel.Find(id);
                varrializer = new JavaScriptSerializer();
                = serializer.Serialize(hoteltobepublished);



                return result;
            }*/

        public string Hotels()
        {
            Entities entity = new Entities();

            string response = "";

            foreach (vaHotel hotel in entity.Hotel)
            {
                response += hotel.ID + "," + hotel.Name + ":";
            }

            response = response.Substring(0, response.Length - 1);

            return response;
        }
    }


    public class DataHotel
    {
        public List<JsonHotel> Hotels { get; set; }
    }    


}