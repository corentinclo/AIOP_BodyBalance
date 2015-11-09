using BodyBalance.Models;
using BodyBalance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BodyBalance.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private IUserServices userServices;
        private ITokenServices tokenServices;

        // GET: /Users
        [Route("Users")]
        [HttpGet]
        public UsersController(IUserServices user,ITokenServices token)
        {
            this.userServices = user;
            this.tokenServices = token;
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
            
        }

        // GET: api/Users/{user-id}
        public UserModel Get(string userid)
        {
            return Ok.;
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
