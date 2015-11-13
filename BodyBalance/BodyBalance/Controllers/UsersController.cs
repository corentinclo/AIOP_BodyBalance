﻿using BodyBalance.Models;
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
    [Authorize]
    public class UsersController : ApiController
    {
        private IUserServices userServices;
        private ITokenServices tokenServices;

        
        public UsersController(IUserServices user,ITokenServices token)
        {
            this.userServices = user;
            this.tokenServices = token;
        }

        // GET: /Users
        [Route("Users")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var users = this.userServices.FindAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("Users/{userid}")]
        // GET: api/Users/{user-id}
        public IHttpActionResult Get(string userid)
        {
            var user = this.userServices.FindUserById(userid);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        // POST: api/Users
        public IHttpActionResult Post([FromBody]UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid user supplied");
            }

            var createResult = userServices.CreateUser(user);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("User created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The user already exists");
            }
            return InternalServerError();
        }

        // PUT: api/Users/{user-id}
        [HttpPut]
        [Route("Users/{userid}")]
        public IHttpActionResult Put(string userid, [FromBody]UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid user supplied");
            }

            if (userServices.FindUserByIdAndPassword(user.UserId, user.Password) == null)
            {
                //TODO: Renvoyer 403 Forbidden
                return BadRequest("Wrong password");
            }

            var updateResult = userServices.UpdateUser(user);
            
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
                var ex = new Exception("Make Sure that your userId exists");
                return InternalServerError(ex);
            }

            return InternalServerError();


        }

        // DELETE: api/Users/{user-id}
        [HttpDelete]
        public IHttpActionResult Delete(string userid)
        {
            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return NotFound();
            }

            var deleteResult = userServices.DeleteUser(user);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }

            return InternalServerError();
        }


        // GET: /Users/{user-id}/Products
        [HttpGet]
        [Route("Users/{userid}/Products")]
        public IHttpActionResult GetProducts(string userid)
        {
            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listProducts = userServices.FindAllProductsOfUser(userid);

            return Ok(listProducts);
        }
    }
}
