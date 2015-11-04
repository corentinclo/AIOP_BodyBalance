using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BodyBalance.Controllers
{
    public class UsersController : ApiController
    {
        // GET: api/Users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/{user-id}
        public string Get(int userid)
        {
            return "value";
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users/{user-id}
        public void Put(int userid, [FromBody]string value)
        {
        }

        // DELETE: api/Users/{user-id}
        public void Delete(int userid)
        {
        }
    }
}
