using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using store_api_test.DataMapping;


namespace store_api_test.Models
{
	public class PortalCatalog
	{
		public int portalID { get; set; }
		public int portalCatalogID { get; set; }
		public int catalogID { get; set; }
		public int localeID { get; set; }

		public string theme { get; set; }
		public string name { get; set; }
		public string CSS { get; set; }


		// addiitonal 
		public int displayOrder { get; set; }
		public bool primary { get; set; }
		public bool isActive { get; set; }


		public IEnumerable<PortalCatalog> ReadDB(int? pID)
		{
			IEnumerable<PortalCatalog> iCatalog;
			PortalCatalogDataContext db = new PortalCatalogDataContext();
			CatalogDataContext dbCat = new CatalogDataContext();

			iCatalog = db.ZNodePortalCatalogs.AsEnumerable()
						.Join( dbCat.ZNodeCatalogs,
								_PCat => _PCat.CatalogID,
								_CData => _CData.CatalogID,
								(_PCat, _CData) => new
								{
									_CData.Name,
									_CData.IsActive,
									_PCat.CatalogID,
									_PCat.LocaleID,
									_PCat.PortalCatalogID,
									_PCat.PortalID,
									_PCat.Theme,
									_PCat.CSS
								}
							)
							.Select(row => new PortalCatalog
							{
								catalogID = row.CatalogID,
								CSS = row.CSS,
								portalID = row.PortalID,
								localeID = row.LocaleID,
								portalCatalogID = row.PortalCatalogID,
								name = row.Name,
								theme = row.Theme,
								isActive = row.IsActive
							});

			if (pID.HasValue)
			{
				iCatalog = iCatalog.Where(row => row.portalID == pID);
			}

			return iCatalog;
		}
	}

}