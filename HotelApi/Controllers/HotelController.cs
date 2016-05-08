using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace HotelApi.Controllers
{
    using System;
    using System.Web.Mvc;
    using Moodroon.DataLayer.Core;
    using Moodroon.DataLayer.Core.DTO;
    using Moodroon.DataLayer.Core.Services;
    
    [System.Web.Http.RoutePrefix("api/hotel")]
    public class HotelController : ApiController
    {
        private readonly HotelService _hotelService;

        public HotelController()
        {
            this._hotelService = new HotelService(UnityFactory.Instance.Container);
        }

        [ActionName("GetAllHotels")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAllHotels()
        {
            try
            {
                var hotels = this._hotelService.GetAllHotels();
                return Json(hotels);
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception);
            }
        }

        [ActionName("DeleteHotel")]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteHotel([FromUri]int id)
        {
            try
            {
                this._hotelService.DeleteHotel(id);
                return this.Ok("Success deleted");
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [ActionName("AddHotel")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult AddHotel(HotelDto hotelDto)
        {
            try
            {
                this._hotelService.AddHotel(hotelDto);
                return this.Ok("Seccess added");
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception);
            }
        }



        [ActionName("GetHotel")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetHotel([FromUri]int id)
        {
            try
            {
                var hotels = this._hotelService.GetHotel(id);
                return this.Ok(hotels);
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception);
            }
        }

        [ActionName("GetHotelRooms")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetHotelRooms([FromUri]int id)
        {
            try
            {
                var hotels = this._hotelService.GetHotelRooms(id);
                return this.Ok(hotels);
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception);
            }
        }

        [HttpPost]
        [Route("GetFreeHotels")]
        [ResponseType(typeof(List<HotelDto>))]
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

        [ActionName("GetHotelConditions")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetHotelConditions([FromUri]int id)
        {
            try
            {
                var hotels = this._hotelService.GetHotelConditions(id);
                return this.Ok(hotels);
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception);
            }
        }
    }
}
