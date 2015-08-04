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
    public class ProductCategoryController : ApiController
    {
		ProductCategory catObject = new ProductCategory();

		public IHttpActionResult GetProductCategories(int id)
		{
			IEnumerable<ProductCategory> product = catObject.ReadDB(id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}



		public IEnumerable<ProductCategory> GetAllProductCategories()
		{
			IEnumerable<ProductCategory> data = catObject.ReadDB(null);
			return data;
		}



    }
}
