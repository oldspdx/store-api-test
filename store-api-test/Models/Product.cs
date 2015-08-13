using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using store_api_test.DataMapping;

namespace store_api_test.Models
{
	public class Product
	{
		public int productID { get; set; }
		public int productTypeID { get; set; }
		public int displayOrder { get; set; }
		public int productCategoryID { get; set; }
		public int maxQty { get; set; }
		public int accountID { get; set; }
		public int portalID { get; set; }
		public int categoryID { get; set; }

		public decimal weight { get; set; }
		public decimal length { get; set; }
		public decimal width { get; set; }
		public decimal height { get; set; }
		public decimal? retailPrice { get; set; }
		public decimal? wholesalePrice { get; set; }
		public decimal? salePrice { get; set; }

		public bool isActive { get; set; }
		public bool isOnSale { get; set; }

		public string title { get; set; }
		public string shortDescription { get; set; }
		public string description { get; set; }
		public string productNumber { get; set; }
		public string imageFile { get; set; }
		public string imageAltTag { get; set; }
		public string SEOTitle { get; set; }
		public string keywords { get; set; }
		public string SEODescription { get; set; }
		public string SEOKeywords { get; set; }

		public DateTime? dateStart { get; set; }
		public DateTime? dateEnd { get; set; }


		public IEnumerable<Product> ReadDB(int? pID, int? catID)
		{
			IEnumerable<Product> iProduct;
			ProductDataContext dbProducts = new ProductDataContext();
			ProductCategoryDataContext dbProdCategories = new ProductCategoryDataContext();
			iProduct = dbProducts.ZNodeProducts.AsQueryable()
				.Join
					(dbProdCategories.ZNodeProductCategories,
						_Products => _Products.ProductID,
						_ProductCategory => _ProductCategory.ProductID,

						(_Products, _ProductCategory) => new
						{
							_Products.Name,
							_Products.PortalID,
							_Products.ShortDescription,
							_Products.Description,
							_Products.ImageFile,
							_Products.ImageAltTag,
							_Products.ActiveInd,
							_Products.RetailPrice,
							_Products.SalePrice,
							_Products.ProductNum,
							_Products.ProductID,
							_ProductCategory.CategoryID,
							_ProductCategory.DisplayOrder,
							_ProductCategory.BeginDate,
							_ProductCategory.EndDate,
							_ProductCategory.MasterPage,
							_ProductCategory.Theme,
							_ProductCategory.CSS,
							_ProductCategory.ProductCategoryID
						}
					)
					.Select(row => new Product
					{
						productID = row.ProductID,
						shortDescription = row.ShortDescription,
						description = row.Description,
						productNumber = row.ProductNum,
						imageFile = row.ImageFile,
						productCategoryID = row.CategoryID,
						displayOrder = (!row.DisplayOrder.HasValue) ? 0 : (int)row.DisplayOrder,
						imageAltTag = row.ImageAltTag,
						title = row.Name,
						categoryID = row.CategoryID,
						salePrice = row.SalePrice,
						retailPrice = row.RetailPrice,
						isOnSale = (row.SalePrice.HasValue) ? true : false,
						portalID = (!row.PortalID.HasValue) ? 0 : (int)row.PortalID
					});
			if (pID.HasValue)
			{
				iProduct = iProduct.Where(row => row.productID == pID);
			}

			if (catID.HasValue)
			{
				iProduct = iProduct.Where(row => row.categoryID == catID);
			}

		//	dbProdCategories.Dispose();
		//	dbProducts.Dispose();

			return iProduct;
		}




	}
}