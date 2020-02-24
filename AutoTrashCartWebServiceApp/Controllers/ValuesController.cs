﻿using System.Collections.Generic;
using System.Web.Http;
using AutoTrashCartWebServiceApp.Filters;

namespace AutoTrashCartWebServiceApp.Controllers
{
    public class ValuesController : ApiController
    {
        // GET: api/Values
        [RouteTimerFilter("GetAllValues")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}
