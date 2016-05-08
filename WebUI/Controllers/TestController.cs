using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moodroon.DataLayer.Core;
using Moodroon.DataLayer.Core.DTO;
using Moodroon.DataLayer.Core.Services;

namespace WebUI.Controllers
{
    public class TestController : Controller
    {
        private readonly OrderService _orderService;

        public TestController()
        {
            this._orderService = new OrderService(UnityFactory.Instance.Container);
        }
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllOrders()
        {
            return this.Json(this._orderService.GetAllOrders(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteOrder(int? id)
        {
            if (id != null)
            {
                this._orderService.DeleteOrder(id.Value);
            }
            return this.Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrder(OrderDto order)
        {
            if (order.ArrivalDate >= order.LeaveDate)
            {
                return new HttpStatusCodeResult(500);
            }
            var res = this._orderService.AddOrder(order);
            if (!res)
            {
                return new HttpStatusCodeResult(500);
            }
            return this.Json(this._orderService.GetAllOrders(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditOrder(OrderDto order)
        {
            if (order.ArrivalDate >= order.LeaveDate)
            {
                return new HttpStatusCodeResult(500);
            }
            var res = this._orderService.EditOrder(order);
            if (!res)
            {
                return new HttpStatusCodeResult(500);
            }
            return this.Json(this._orderService.GetAllOrders(), JsonRequestBehavior.AllowGet);
        }
    }
}