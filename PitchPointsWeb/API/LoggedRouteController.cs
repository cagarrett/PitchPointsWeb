using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PitchPointsWeb.API
{
    public class LoggedRouteController : ApiController
    {
        // GET: api/LogedRoute
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LogedRoute/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LogedRoute
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LogedRoute/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LogedRoute/5
        public void Delete(int id)
        {
        }
    }
}
