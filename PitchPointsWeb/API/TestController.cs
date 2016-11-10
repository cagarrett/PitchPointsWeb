using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PitchPointsWeb.API
{
    public class TestController : ApiController
    {

        public HttpResponseMessage GetData()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Test");
        }

        public HttpResponseMessage GetData(int position)
        {
            List<UInt32> stuff = new List<uint>();
            for (uint i = 0; i < position; i++)
            {
                stuff.Add(i);
            }
            return Request.CreateResponse(HttpStatusCode.OK, stuff);
        }

    }
}
