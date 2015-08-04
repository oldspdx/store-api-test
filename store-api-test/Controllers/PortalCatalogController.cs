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
    public class PortalCatalogController : ApiController
    {

		PortalCatalog catObject = new PortalCatalog();

		public IHttpActionResult GetPortalCatalog(int id)
		{

			IEnumerable<PortalCatalog> catalog = catObject.ReadDB(id);
			if (catalog == null)
			{
				return NotFound();
			}
			return Ok(catalog);
		}



		public IEnumerable<PortalCatalog> GetAllPortalCatalogs()
		{
			IEnumerable<PortalCatalog> data = catObject.ReadDB(null);
			return data;
		}


		public IEnumerable<PortalCatalog> GetCatalogs(int id)
		{
			IEnumerable<PortalCatalog> catalog = catObject.ReadDB(id);
			return (catalog);
		}


	}
    
}
