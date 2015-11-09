using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BodyBalance.Controllers
{
    [Authorize]
    public class ActivitiesController : ApiController
    {
        // GET: Activities
        [Route("Activities")]
        public IHttpActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            return Ok();
        }

        // GET: api/Activities/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Activities
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Activities/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Activities/5
        public void Delete(int id)
        {
        }
    }
}
