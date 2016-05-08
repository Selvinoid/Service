using System;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Moodroon.DataLayer.Core;
using Moodroon.DataLayer.Core.DTO;
using Moodroon.DataLayer.Core.Services;

namespace HotelApi.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private readonly OrderService _orderService;
        private readonly HotelService _hotelService;

        public OrdersController()
        {
            this._orderService = new OrderService(UnityFactory.Instance.Container);
            this._hotelService = new HotelService(UnityFactory.Instance.Container);
        }
        [HttpGet]
        [System.Web.Mvc.ActionName("GetOrders")]
        public IHttpActionResult GetOrders()
        {
            return this.Ok(this._orderService.GetUserOrdersDtos(Convert.ToInt32(this.User.Identity.GetUserId())));
        }

        [HttpGet]
        [System.Web.Mvc.ActionName("GetOrdersCount")]
        public IHttpActionResult GetOrdersCount()
        {
            return this.Ok(this._orderService.GetUserOrdersDtos(Convert.ToInt32(this.User.Identity.GetUserId())).Count);
        }

        [HttpGet]
        [System.Web.Mvc.ActionName("DeleteOrder")]
        public IHttpActionResult DeleteOrder(int id)
        {
            this._orderService.DeleteOrder(Convert.ToInt32(this.User.Identity.GetUserId()), id);
            return this.Ok("ok");
        }

        [System.Web.Mvc.ActionName("addOrder")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult AddOrder([FromBody]OrderDto order)
        {
            order.ArrivalDate = DateTime.Now + TimeSpan.FromDays(1);
            order.LeaveDate = DateTime.Now + TimeSpan.FromDays(2);
            order.CountOfDays = 2;
            order.UserName = this.User.Identity.GetUserName();
            order.TotalPrice = order.CountOfDays * 100;
            var hotel = this._hotelService.GetAllHotels().FirstOrDefault(p => p.Rooms.Count >= 1);
            order.HotelName = hotel.Name;
            order.RoomNumber = hotel.Rooms.FirstOrDefault().Number;
            var res = this._orderService.AddOrder(order);
            if (res)
            {
                return this.Ok("Added");
            }
            return this.BadRequest("Not added");
        }
    }
}
