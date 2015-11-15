using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BodyBalance.Controllers
{
    public class BasketsController : ApiController
    {
        // GET: Baskets
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: Baskets/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: Baskets
        public void Post([FromBody]string value)
        {
        }

        // PUT: Baskets/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: Baskets/5
        public void Delete(int id)
        {
        }
    }
}
