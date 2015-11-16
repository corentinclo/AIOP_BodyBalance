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
    public class MembersController : ApiController
    {
        private IMemberServices memberServices;
        private IUserServices userServices;
        public MembersController(IMemberServices memberServices,
            IUserServices user)
        {
            this.memberServices = memberServices;
            this.userServices = user;
        }

        // GET: /Members
        [HttpGet]
        public IHttpActionResult Get()
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var members = memberServices.FindAllMembers();
            return Ok(members);
        }

        // GET: /Members/{member_id}
        [HttpGet]
        [Route("Members/{member_id}")]
        public IHttpActionResult Get(string member_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var member = memberServices.FindMemberById(member_id);

            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        // POST: Members
        [HttpPost]
        public IHttpActionResult Post([FromBody] MemberModel model)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid member supplied");
            }

            var createResult = memberServices.CreateMember(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Member created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The member already exists");
            }
            return InternalServerError();
        }

        // PUT: Members/{member_id}
        [HttpPut]
        [Route("Members/{member_id}")]
        public IHttpActionResult Put(string member_id, [FromBody]MemberModel model)
        {
            var member = memberServices.FindMemberById(model.UserId);
            if (member == null)
            {
                return BadRequest("Invalid contributor id supplied");
            }


            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)) && member.UserId != user.UserId)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid member supplied");
            }

            var updateResult = memberServices.UpdateMember(model);

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
                var ex = new Exception("Make Sure that your member id exists");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: Members/{member_id}
        [HttpDelete]
        [Route("Members/{member_id}")]
        public IHttpActionResult Delete(string member_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var member = memberServices.FindMemberById(member_id);
            if (member == null)
            {
                return NotFound();
            }

            var deleteResult = memberServices.DeleteMember(member);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}
