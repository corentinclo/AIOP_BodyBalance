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
    public class CategoriesController : ApiController
    {
        private ICategoryServices categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        // GET: Categories
        [HttpGet]
        public IHttpActionResult Get()
        {
            var listCategories = categoryServices.FindAllCategories();
            return Ok(listCategories);
        }


        // GET: Categories/{category_id}
        [HttpGet]
        [Route("Categories/{category_id}")]
        public IHttpActionResult Get(string category_id)
        {
            var category = categoryServices.FindCategoryWithId(category_id);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: Categories
        [HttpPost]
        public IHttpActionResult Post([FromBody]CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid category supplied");
            }

            var createResult = categoryServices.CreateCategory(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Category created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The category already exists");
            }
            return InternalServerError();
        }

        // PUT: Categories/{category_id}
        [HttpPut]
        [Route("Categories/{category_id}")]
        public IHttpActionResult Put(string category_id, [FromBody]CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid category supplied");
            }

            var category = categoryServices.FindCategoryWithId(category_id);

            if (category == null)
            {
                return NotFound();
            }
            if (category.CategoryId != model.CategoryId)
            {
                return BadRequest("Invalid category id supplied");
            }

            var updateResult = categoryServices.UpdateCategory(model);
            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Category updated successfully");
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: Categories/{category_id}
        [HttpDelete]
        [Route("Categories/{category_id}")]
        public IHttpActionResult Delete(string category_id)
        {
            var category = categoryServices.FindCategoryWithId(category_id);
            if (category == null)
            {
                return BadRequest("Bad category id supplied");
            }

            var deleteResult = categoryServices.DeleteCategory(category);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Category deleted successfully");
            }
            return InternalServerError();
        }

        // GET: Categories/{category_id}/Products
        [HttpGet]
        [Route("Categories/{category_id}/Products")]
        public IHttpActionResult GetProducts(string category_id)
        {
            var category = categoryServices.FindCategoryWithId(category_id);
            if (category == null)
            {
                return BadRequest("Bad category id supplied");
            }

            var listProducts = categoryServices.FindAllProductsOfCategory(category_id);
            return Ok(listProducts);
        }

    }
}
