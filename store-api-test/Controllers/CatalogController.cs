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
    public class CatalogController : ApiController
    {

		Catalog catObject = new Catalog();

        public IHttpActionResult GetCatalog(int id)
        {
			IEnumerable<Catalog> catalog = catObject.ReadDB(id);
            if (catalog == null)
            {
                return NotFound();
            }
            return Ok(catalog);
        }




        public IEnumerable<Catalog> GetAllCatalogs()
        {
			IEnumerable<Catalog> data = catObject.ReadDB(null);
			return data;
        }



    }
}
