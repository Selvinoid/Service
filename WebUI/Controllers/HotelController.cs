using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Amazon.S3;
using Amazon.S3.Model;
using Moodroon.DataLayer.Core;
using Moodroon.DataLayer.Core.DTO;
using Moodroon.DataLayer.Core.Services;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HotelController : Controller
    {
        private readonly HotelService _hotelService;

        public HotelController()
        {
            this._hotelService = new HotelService(UnityFactory.Instance.Container);
        }
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddHotel(HotelDto hotelDto)
        {
            if (hotelDto != null)
            {
                var res= this._hotelService.AddHotel(hotelDto);
                if (!res)
                {
                    return new HttpStatusCodeResult(500);
                }
                return this.Json("Succees", JsonRequestBehavior.AllowGet);
            }
            return this.Json("Empty", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditHotel(HotelDto hotelDto)    
        {
            if (hotelDto != null)
            {
                var res = this._hotelService.EditHotel(hotelDto);
                if (!res)
                {
                    return new HttpStatusCodeResult(500);
                }
                return this.Json("Succees", JsonRequestBehavior.AllowGet);
            }
            return this.Json("Empty", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveImage(int? id, string image)
        {
            if (id != null && !string.IsNullOrEmpty(image))
            {
                this._hotelService.RemoveImage(id.Value, image);
            }
            return this.Json("Succes", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetHotels()
        {
            return this.Json(this._hotelService.GetAllHotels(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteHotel(int? id)
        {
            if (id != null)
            {
                this._hotelService.DeleteHotel(id.Value);
            }
            return this.Json("Succes", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SetHotelId(int? id)
        {
            if (id != null)
            {
                this.Session["CurrentHotelId"] = id;
            }
            return this.Json("Succes", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddRoom(RoomDto room)
        {
            if (room != null)
            {
                var result = this._hotelService.AddRoom(room, (int)this.Session["CurrentHotelId"]);
                if (!result)
                {
                    return new HttpStatusCodeResult(500);
                }
                return this.Json("Succees", JsonRequestBehavior.AllowGet);
            }
            return this.Json("Room is empty", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public RedirectToRouteResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null)
            {

                var s3Config = new Amazon.S3.AmazonS3Config { ServiceURL = AppConfiguration.AppConfiguration.Instance.ServiceUrl };
                using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(
                    AppConfiguration.AppConfiguration.Instance.AccessKey,
                   AppConfiguration.AppConfiguration.Instance.SecretKey,
                   s3Config))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = "imagesforhotel",
                        Key = file.FileName,
                        InputStream = file.InputStream,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    client.PutObject(request);
                    var url = "https://s3.amazonaws.com/" + request.BucketName + "/" + file.FileName;
                    this._hotelService.AddHotelImage((int)this.Session["CurrentHotelId"], url);
                }
            }
            return this.RedirectToAction("Index");
        }
        [HttpPost]
        public RedirectToRouteResult AddCondition(string name, HttpPostedFileBase file)
        {
            if (file != null)
            {

                var s3Config = new Amazon.S3.AmazonS3Config { ServiceURL = AppConfiguration.AppConfiguration.Instance.ServiceUrl };
                using (var client = Amazon.AWSClientFactory.CreateAmazonS3Client(
                    AppConfiguration.AppConfiguration.Instance.AccessKey,
                   AppConfiguration.AppConfiguration.Instance.SecretKey,
                   s3Config))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = "imagesforhotel",
                        Key = file.FileName,
                        InputStream = file.InputStream,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    client.PutObject(request);
                    var url = "https://s3.amazonaws.com/" + request.BucketName + "/" + file.FileName;

                    this._hotelService.AddCondition((int)this.Session["CurrentHotelId"], name, url);
                }
            }
            return this.RedirectToAction("Index");
        }
    }
}