using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoTrashCartWebServiceApp.Interfaces;
using AutoTrashCartWebServiceApp.Models;

namespace AutoTrashCartWebServiceApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/AutoTrashCartMobile")]
    public class AutoTrashCartMobileController : ApiController
    {
        private readonly IAutoTrashCartRepository _autoTrashCartRepository;

        public AutoTrashCartMobileController(IAutoTrashCartRepository autoTrashCartRepository)
        {
            _autoTrashCartRepository = autoTrashCartRepository;
        }

        [Route("GetSchedule")]
        public Schedule GetSchedule(string scheduleId)
        {
            return _autoTrashCartRepository.GetSchedule(scheduleId);
        }

        [Route("SetSchedule")]
        public IHttpActionResult SetSchedule(Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool response = _autoTrashCartRepository.SetSchedule(schedule);

            if (response)
                return Ok();

            return BadRequest("Something went wrong!");
        }

        [Route("GetPath")]
        public Path GetPath(string pathId)
        {
            return _autoTrashCartRepository.GetPath(pathId);
        }

        [Route("SetPath")]
        public IHttpActionResult SetPath(Path path)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool response = _autoTrashCartRepository.SetPath(path.PathId, path.S, path.E, path.Leftb, path.Rightb, path.Centerl, path.So, path.Eo);

            if (response)
                return Ok();

            return BadRequest("Something went wrong!");
        }

        [Route("GetSync")]
        public Sync GetSync(string syncId)
        {
            return _autoTrashCartRepository.GetSync(syncId);
        }

        [Route("SetSync")]
        public IHttpActionResult SetSync(Sync sync)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool response = _autoTrashCartRepository.SetSync(sync.SyncId, sync.Schedule, sync.Path);

            if (response)
                return Ok();

            return BadRequest("Something went wrong!");
        }
    }
}
