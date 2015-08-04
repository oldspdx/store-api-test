using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using store_api_test.DataMapping;

namespace store_api_test.Models
{
	public class ProductCategory
	{
		public int productCategoryID { get; set; }
		public int productID { get; set; }
		public int categoryID { get; set; }
		public int displayOrder { get; set; }

		public bool isActive { get; set; }

		public DateTime? dateStart { get; set; }
		public DateTime? dateEnd { get; set; }

		public string theme { get; set; }
		public string masterPage { get; set; }
		public string CSS { get; set; }


		
		public IEnumerable<ProductCategory> ReadDB(int? catID)
		{
			IEnumerable<ProductCategory> iProduct;
			ProductCategoryDataContext db = new ProductCategoryDataContext();

			iProduct = db.ZNodeProductCategories.AsEnumerable()
							.Select(row => new ProductCategory
							{
								productID = row.ProductID,
								masterPage = row.MasterPage,
								theme = row.Theme,
								CSS = row.CSS,
								dateStart = row.BeginDate,
								dateEnd = row.EndDate,
								displayOrder = (!row.DisplayOrder.HasValue) ? 0 : (int)row.DisplayOrder,
								categoryID = row.CategoryID,
								isActive = row.ActiveInd
							}).ToList();
			if (catID.HasValue)
			{
				iProduct = iProduct.Where(row => row.categoryID == catID);
			}

			return iProduct;
		}

		
	}
}