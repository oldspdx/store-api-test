using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using store_api_test.DataMapping;

namespace store_api_test.Models
{
    public class Catalog
    {
        public int catalogID { get; set; }
        public bool isActive { get; set; }
        public string title { get; set; }
        public int portalID { get; set; }


		public IEnumerable<Catalog> ReadDB(int? pID)
		{
			IEnumerable<Catalog> iCatalog;
			CatalogDataContext db = new CatalogDataContext();
			iCatalog = db.ZNodeCatalogs.AsEnumerable()
							.Select(row => new Catalog
							{
								catalogID = row.CatalogID,
								isActive = row.IsActive,
								title = row.Name,
								portalID = (!row.PortalID.HasValue) ? 0 : (int)row.PortalID
							});

			if (pID.HasValue)
			{
				iCatalog = iCatalog.Where(row => row.portalID == pID);
			}
			return iCatalog;
		}

    }
}