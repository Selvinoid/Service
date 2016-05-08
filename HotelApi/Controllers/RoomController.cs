using System.Web.Http;
using System.Web.Http.Cors;

namespace HotelApi.Controllers
{
    using System;
    using System.Web.Mvc;

    using Moodroon.DataLayer.Core;
    using Moodroon.DataLayer.Core.Services;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/room")]
    public class RoomController : ApiController
    {
        private readonly RoomService roomService;

        public RoomController()
        {
            this.roomService = new RoomService(UnityFactory.Instance.Container);
        }

        [ActionName("GetRoom")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetRoom([FromUri]int id)
        {
            try
            {
                var hotels = this.roomService.GetRoom(id);
                return this.Ok(hotels);
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception);
            }
        }

        [ActionName("DeleteRoom")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult DeleteRoom([FromUri]int id)
        {
            try
            {
                this.roomService.DeleteRoom(id);
                return this.Ok("Success");
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception);
            }
        }

    }
}
