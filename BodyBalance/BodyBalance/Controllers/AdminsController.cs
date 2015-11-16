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
    public class AdminsController : ApiController
    {
        private IAdminServices adminServices;
        private IUserServices userServices;
        public AdminsController(IAdminServices admin, IUserServices user)
        {
            this.adminServices = admin;
            this.userServices = user;
        }

        // GET: Admins
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

            var admins = adminServices.FindAllAdmins();
            return Ok(admins);
        }

        // GET: Admins/{adminid}
        [HttpGet]
        [Route("Admins/{adminid}")]
        public IHttpActionResult Get(string adminid)
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

            var admin = adminServices.FindAdminById(adminid);

            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        // POST: Admins
        public IHttpActionResult Post([FromBody] AdminModel model)
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
                return BadRequest("Invalid admin supplied");
            }

            var createResult = adminServices.CreateAdmin(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Admin created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The admin already exists");
            }
            return InternalServerError();
        }

        // DELETE: Admins/{adminid}
        [HttpDelete]
        [Route("Admins/{adminid}")]
        public IHttpActionResult Delete(string adminid)
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

            var admin = adminServices.FindAdminById(adminid);
            if (admin == null)
            {
                return NotFound();
            }

            var deleteResult = adminServices.DeleteAdmin(admin);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}