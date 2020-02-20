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
    [RoutePrefix("api/AutoTrashCartAdmin")]
    public class AutoTrashCartAdminController : ApiController
    {
        private readonly IAutoTrashCartRepository _autoTrashCartRepository;

        public AutoTrashCartAdminController(IAutoTrashCartRepository autoTrashCartRepository)
        {
            _autoTrashCartRepository = autoTrashCartRepository;
        }

        [Route("GetAppLogs")]
        public IEnumerable<AutoTrashCartApiLog> GetTrashCartApplicationLogs()
        {
            return _autoTrashCartRepository.GetApplicationLogs();
        }

        [Route("GetAppErrorLogs")]
        public IEnumerable<AutoTrashCartApiError> GetTrashCartErrorLogs()
        {
            return _autoTrashCartRepository.GetApplicationErrors();
        }
    }
}
