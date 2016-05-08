using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Moodroon.DataLayer.Core;
using Moodroon.DataLayer.Core.DTO;
using Moodroon.DataLayer.Core.Services;

namespace HotelApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/reservation")]
    public class ReservationController : ApiController
    {
        private readonly HotelService _hotelService;

        public ReservationController()  
        {
            this._hotelService = new HotelService(UnityFactory.Instance.Container);
        }



        [HttpGet]
        [System.Web.Http.ActionName("GetFreeHotels")]
        public IHttpActionResult GetFreeHotels(DateTime? from, DateTime? to)
        {
            try
            {
                if (from.Value < DateTime.Now || to.Value < DateTime.Now || to.Value < from.Value)
                {
                    return this.BadRequest("Date range is not valid");
                }
                var hotels = this._hotelService.GetFreeHotels(from, to);
                return this.Ok(hotels);
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception);
            }
        }
    }
}
