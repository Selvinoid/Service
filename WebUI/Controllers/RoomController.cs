using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RoomController : Controller
    {
        private readonly RoomService _roomService;

        public RoomController()
        {
            this._roomService = new RoomService(UnityFactory.Instance.Container);
        }
        // GET: Room
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult GetAllRooms()
        {
            return this.Json(this._roomService.GetAllRooms(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditRoom(RoomDto room)   
        {
            if (room != null)
            {
                var result = this._roomService.EditRoom(room);
                if (!result)
                {
                    return new HttpStatusCodeResult(500);
                }
                return this.Json("Success", JsonRequestBehavior.AllowGet);
            }
            return this.Json("Room is empty", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteRoom(int? id)
        {
            if (id != null)
            {
                this._roomService.DeleteRoom(id.Value);
            }
            return this.Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveImage(int? id, string url)
        {
            if (id != null && !string.IsNullOrEmpty(url))
            {
                this._roomService.RemoveIamge(id.Value, url);
            }
            return this.Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public RedirectToRouteResult UploadImage(HttpPostedFileBase file, int? id)
        {
            if (file != null && id != null)
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
                    this._roomService.UploadImage(id.Value, url);
                }
            }
            return this.RedirectToAction("Index");
        }
    }
}