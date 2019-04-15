using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecuroteckWebApplication.Controllers
{
    public class TalkBackController : ApiController
    {
        [ActionName("Hello")]
        public string Get()
        {
            string response = "Hello World";
            return response;
        }

        [ActionName("Sort")]
        public int[] Get([FromUri]int[] integers)
        {
            Array.Sort(integers);
            return integers;
        }

    }
}
