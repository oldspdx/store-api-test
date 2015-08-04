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
    public class PortalController : ApiController
    {

		public IHttpActionResult GetPortal(int id)
		{


			//Search s = new Search();
			//s.test("mug");
			IEnumerable<Portal> iPortal = ReadDB();
			var portal = (from tempdata in iPortal where tempdata.portalID==id select tempdata);
			if (portal == null)
			{
				return NotFound();
			}
			return Ok(portal);
		}


		
		public IHttpActionResult GetPortalByName(String title)
		{
			title = title.ToLower();

			Search s = new Search();
			s.test("mug");

			IEnumerable<Portal> iPortal = ReadDB();
			var portal = (from tempdata in iPortal where tempdata.storeName.ToLower().Contains(title) select tempdata);
			if (portal == null)
			{
				return NotFound();
			}
			return Ok(portal);
		}




		//public IEnumerable<Portal> GetAllPortals()
		//{
		//	IEnumerable<Portal> data = ReadDB();
		//	return data;
		//}




		private IEnumerable<Portal> ReadDB()
		{
			IEnumerable<Portal> iPortal;
			PortalDataContext db = new PortalDataContext();

			iPortal = db.ZNodePortals.AsEnumerable()
					.Select(row => new Portal
					{
						portalID = row.PortalID,
						isActive = row.ActiveInd,
						storeName = row.StoreName,
						companyName = row.CompanyName
					}).ToList();
			
			return iPortal;
		}

    }
}
