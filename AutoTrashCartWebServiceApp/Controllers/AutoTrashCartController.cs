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
    [RoutePrefix("api/AutoTrashCart")]
    public class AutoTrashCartController : ApiController
    {
        private readonly IAutoTrashCartRepository _autoTrashCartRepository;

        public AutoTrashCartController(IAutoTrashCartRepository autoTrashCartRepository)
        {
            _autoTrashCartRepository = autoTrashCartRepository;
        }

        [Route("GetSchedule")]
        public Schedule GetSchedule(string token)
        {
            return _autoTrashCartRepository.GetSchedule(token);
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
        public Path GetPath(string token)
        {
            return _autoTrashCartRepository.GetPath(token);
        }

        [Route("SetPath")]
        public IHttpActionResult SetPath(Path path)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool response = _autoTrashCartRepository.SetPath(path.Token, path.S, path.E, path.Leftb, path.Rightb, path.Centerl, path.So, path.Eo);

            if (response)
                return Ok();

            return BadRequest("Something went wrong!");
        }

        [Route("GetSync")]
        public Sync GetSync(string token)
        {
            return _autoTrashCartRepository.GetSync(token);
        }

        [Route("SetSync")]
        public IHttpActionResult SetSync(Sync sync)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool response = _autoTrashCartRepository.SetSync(sync.Token, sync.Schedule, sync.Path);

            if (response)
                return Ok();

            return BadRequest("Something went wrong!");
        }
    }
}
