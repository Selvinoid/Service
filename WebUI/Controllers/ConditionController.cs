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
    public class ConditionController : Controller
    {
        private readonly ConditionService _conditionService;

        public ConditionController()
        {
            this._conditionService = new ConditionService(UnityFactory.Instance.Container);
        }
        // GET: Condition
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteCondition(int? id)
        {
            if (id != null)
            {
                this._conditionService.DeleteCondition(id.Value);
            }
            return this.Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveImage(int? id, string image)
        {
            if (id != null && !string.IsNullOrEmpty(image))
            {
                this._conditionService.RemoveImage(image, id.Value);
            }
            return this.Json("Succes", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditCondition(ConditionDto conditionDto)
        {
            if (conditionDto != null)
            {
                var res = this._conditionService.EditCondition(conditionDto);
                if (!res)
                {
                    return new HttpStatusCodeResult(500);
                }
                return this.Json("Succees", JsonRequestBehavior.AllowGet);
            }
            return this.Json("Empty", JsonRequestBehavior.AllowGet);
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
                    this._conditionService.UploadImage(url, id.Value);
                }
            }
            return this.RedirectToAction("Index");
        }
    }
}