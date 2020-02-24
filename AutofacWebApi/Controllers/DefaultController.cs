using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutofacWebApi.Controllers
{
    public class DefaultController : ApiController
    {
        private IServiceA _serviceA;
        public DefaultController(IServiceA serviceA)
        {
            _serviceA = serviceA;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {

            return this.Json("");
        }
    }
}
