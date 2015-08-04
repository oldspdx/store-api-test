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
    public class CategoryNodeController : ApiController
    {

		CategoryNode catObject = new CategoryNode();

		public IHttpActionResult GetCategoryNode(int id)
		{
			IEnumerable<CategoryNode> category = catObject.ReadDB(id,null);
			if (category == null)
			{
				return NotFound();
			}
			return Ok(category);
		}



		public IHttpActionResult GetSubCategory(int id, int parent)
		{

			IEnumerable<CategoryNode> category = catObject.ReadDB(id, parent);
			if (category == null)
			{
				return NotFound();
			}
			return Ok(category);
		}




	public IEnumerable<CategoryNode> GetAllCategoryNodes()
		{
			IEnumerable<CategoryNode> data = catObject.ReadDB(null,null);
			return data;
		}





    }
}
