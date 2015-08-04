using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using store_api_test.Models;
using store_api_test.DataMapping;

namespace store_api_test.Controllers
{
    public class CategoryController : ApiController
    {

		Category catObject = new Category();

		public IHttpActionResult GetCategories(int id)
		{
			IEnumerable<Category> category;
			category = catObject.ReadDB(id);
			if (category == null)
			{
				return NotFound();
			}
			return Ok(category);
		}



		public IEnumerable<Category> GetAllCategories()
		{
			IEnumerable<Category> data = catObject.ReadDB(null);
			return data;
		}





    }
}
