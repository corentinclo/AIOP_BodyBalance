using BodyBalance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BodyBalance.Controllers
{
    public class ContributorsController : ApiController
    {
        private IContributorServices contributorServices;
        public ContributorsController(IContributorServices contributorServices)
        {
            this.contributorServices = contributorServices;
        }
        // GET: Contributors
        [HttpGet]
        public IHttpActionResult Get()
        {
            var contributors = contributorServices.FindAllContributors();
            return Ok(contributors);
        }

        // GET: api/Contributors/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Contributors
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Contributors/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Contributors/5
        public void Delete(int id)
        {
        }
    }
}
