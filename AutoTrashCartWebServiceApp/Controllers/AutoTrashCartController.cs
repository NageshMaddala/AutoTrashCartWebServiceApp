using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoTrashCartWebServiceApp.Filters;
using AutoTrashCartWebServiceApp.Interfaces;
using AutoTrashCartWebServiceApp.Models;
using Swashbuckle.Swagger.Annotations;

namespace AutoTrashCartWebServiceApp.Controllers
{
    [RoutePrefix("api/AutoTrashCart")]
    public class AutoTrashCartController : ApiController
    {
        private readonly IAutoTrashCartRepository _autoTrashCartRepository;

        public AutoTrashCartController(IAutoTrashCartRepository autoTrashCartRepository)
        {
            _autoTrashCartRepository = autoTrashCartRepository;
        }

        [Route("GetSchedule")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Schedule))]
        public IHttpActionResult GetSchedule(string scheduleId)
        {
            var schedule = _autoTrashCartRepository.GetSchedule(scheduleId);
            return Ok(schedule);
        }

        [Route("SetSchedule")]
        [ValidateModelState]
        public IHttpActionResult SetSchedule(Schedule schedule)
        {
            bool response = _autoTrashCartRepository.SetSchedule(schedule);

            if (response)
                return Ok();

            return BadRequest("Something went wrong!");
        }

        [Route("GetPath")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Path))]
        public IHttpActionResult GetPath(string pathId)
        {
            var path = _autoTrashCartRepository.GetPath(pathId);
            return Ok(path);
        }

        [Route("SetPath")]
        [ValidateModelState]
        public IHttpActionResult SetPath(Path path)
        {
            bool response = _autoTrashCartRepository.SetPath(path.PathId, path.S, path.E, path.Leftb, path.Rightb, path.Centerl, path.So, path.Eo);

            if (response)
                return Ok();

            return BadRequest("Something went wrong!");
        }

        [Route("GetSync")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Sync))]
        public IHttpActionResult GetSync(string syncId)
        {
            var sync = _autoTrashCartRepository.GetSync(syncId);
            return Ok(sync);
        }

        [Route("SetSync")]
        [ValidateModelState]
        public IHttpActionResult SetSync(Sync sync)
        {
            bool response = _autoTrashCartRepository.SetSync(sync.SyncId, sync.Schedule, sync.Path);

            if (response)
                return Ok();

            return BadRequest("Something went wrong!");
        }
    }
}
