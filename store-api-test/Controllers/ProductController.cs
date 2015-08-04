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
			// TODO: replace this with a more efficient algorithm. Just getting endpoinst in place 
			// and test cases written

			// get all categories for the portal
			PortalCatalogController catObject = new PortalCatalogController();
			IEnumerable<PortalCatalog> catalogs = catObject.GetCatalogs(portalID);

			CategoryNodeController cateNodeDB = new CategoryNodeController();

			//IEnumerable<CategoryNode> cateNode = cateNodeDB.GetCategoryNode(2);

			// now get all pructs for these categories

			IEnumerable<Product> product = prodObject.ReadDB(null, null);
			IEnumerable<Product> productList;

			if (product != null)
			{
				productList = product.Where(row => row.portalID==portalID && row.title.ToLower().Contains(textstring));

			}


			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}



		public IEnumerable<Product> GetAllProducts()
		{
			IEnumerable<Product> data = prodObject.ReadDB(null, null);
			return data;
		}





	

    }
}
