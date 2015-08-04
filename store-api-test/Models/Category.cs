using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using store_api_test.DataMapping;

namespace store_api_test.Models
{
	public class Category
	{
		public int categoryID { get; set; }
		public int displayOrder { get; set; }
		public int portalID { get; set; }

		public bool subCategoryGridVisibleInd { get; set; }
		public bool visibleInd { get; set; }

		public string name { get; set; }
		public string title { get; set; }
		public string shortDescription { get; set; }
		public string description { get; set; }
		public string imageFile { get; set; }
		public string imageAltTag { get; set; }
		public string SEOTitle { get; set; }
		public string SEOKeywords { get; set; }
		public string SEODescription { get; set; }
		public string alternateDescription { get; set; }
		public string custom1 { get; set; }
		public string custom2 { get; set; }
		public string custom3 { get; set; }
		public string SEOURL { get; set; }


		public IEnumerable<Category> ReadDB(int? cID)
		{
			IEnumerable<Category> iCategory;
			CategoryDataContext db = new CategoryDataContext();
			iCategory = db.ZNodeCategories.AsEnumerable()
							.Select(row => new Category
							{
								categoryID = row.CategoryID,
								shortDescription = row.ShortDescription,
								description = row.Description,
								title = row.Title,
								imageFile = row.ImageFile,
								displayOrder = (!row.DisplayOrder.HasValue) ? 0 : (int)row.DisplayOrder,
								imageAltTag = row.ImageAltTag,
								name = row.Name,
								portalID = (!row.PortalID.HasValue) ? 0 : (int)row.PortalID
							});
			if (cID.HasValue)
			{
				iCategory = iCategory.Where(row => row.categoryID == cID);
			}
			return iCategory;
		}

	
	}
}