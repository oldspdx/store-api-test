using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using store_api_test.Models;
using store_api_test.DataMapping;
using store_api_test;



namespace store_api_test.Controllers
{
    public class ProductController : ApiController
    {

		Product prodObject = new Product();

		

		public IHttpActionResult GetProducts(int id)
		{
			IEnumerable<Product> product = prodObject.ReadDB(id, null);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}



		public IHttpActionResult GetProducyByCategory(int categoryID)
		{

			IEnumerable<Product> product = prodObject.ReadDB(null, categoryID);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}


		public IHttpActionResult GetProducyByKeyword(int portalID, string textstring)
		{
			// TODO: replace this with a more efficient algorithm. Just getting endpoints in place 
			// and test cases written

			String sMode = null;
			String sFields = null;

			//optional params of fieldName = all | title | description  mode = all | exact | any
			var modifiers = RequestHelpers.GetQueryStrings(this.Request);

			if (modifiers.Count>0)
			{
				if (modifiers.ContainsKey("mode"))
					sMode = modifiers["mode"];

				if (modifiers.ContainsKey("fieldname"))
					sFields = modifiers["fieldname"];
            }

		
			Search search = new Search();
			search.PortalID = portalID;
            IEnumerable<Product> productList = null; 
			if (search.Keywords(textstring, sFields, sMode))
			{
				productList = search.ProductList;
			}

			if (productList == null)
			{
				return NotFound();
			}
			return Ok(productList);
		}



		public IHttpActionResult GetAllProducts()
		{
			if (this.Request.RequestUri.ToString().Contains("keyword"))
			{
				var message = "No keyword provided";
				return BadRequest(message);

			}

			IEnumerable<Product> data = prodObject.ReadDB(null, null);
			return Ok(data);
		}





	

    }
}
