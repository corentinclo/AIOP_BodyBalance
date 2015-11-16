using BodyBalance.Models;
using BodyBalance.Services;
using BodyBalance.Utilities;
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
        private IUserServices userServices;
        public ContributorsController(IContributorServices contributorServices,
            IUserServices user)
        {
            this.contributorServices = contributorServices;
            this.userServices = user;
        }
        // GET: Contributors
        [HttpGet]
        public IHttpActionResult Get()
        {
            var contributors = contributorServices.FindAllContributors();
            return Ok(contributors);
        }

        // GET: api/Contributors/{contributor_id}
        [HttpGet]
        [Route("Contributors/{contributor_id}")]
        public IHttpActionResult Get(string contributor_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)) || !(userServices.IsManager(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var contributor = contributorServices.FindContributorById(contributor_id);

            if (contributor == null)
            {
                return NotFound();
            }
            return Ok(contributor);
        }

        // POST: Contributors
        public IHttpActionResult Post([FromBody]ContributorModel model)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)) || !(userServices.IsManager(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid contributor supplied");
            }

            var createResult = contributorServices.CreateContributor(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Contributor created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The contributor already exists");
            }
            return InternalServerError();
        }

        // PUT: Contributors/{contributor_id}
        [HttpPut]
        [Route("Contributors/{contributor_id}")]
        public IHttpActionResult Put(string contributor_id, [FromBody]ContributorModel model)
        {
            var contributor = contributorServices.FindContributorById(model.UserId);
            if (contributor == null)
            {
                return BadRequest("Invalid contributor id supplied");
            }


            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (contributor.UserId != user.UserId)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid contributor supplied");
            }

            if (userServices.FindUserByIdAndPassword(contributor.UserId, contributor.Password) == null)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            var updateResult = contributorServices.UpdateContributor(model);

            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            if (updateResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                var ex = new Exception("Make Sure that your contributor id exists");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: api/Contributors/{contributor_id}
        [HttpDelete]
        [Route("Contributors/{contributor_id}")]
        public IHttpActionResult Delete(string contributor_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)) || !(userServices.IsManager(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var contributor = contributorServices.FindContributorById(contributor_id);
            if (contributor == null)
            {
                return NotFound();
            }

            var deleteResult = contributorServices.DeleteContributor(contributor);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}
