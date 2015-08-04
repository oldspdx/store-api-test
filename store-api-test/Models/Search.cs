using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using store_api_test.DataMapping;

namespace store_api_test.Models
{
	public class Search
	{
		private Product product = new Product();
		private IEnumerable<Product> productList;

		public int portalID { get; set; }

		public void test(string searchtext)
		{
			PortalCatalogDataContext dbPortalCatalog = new PortalCatalogDataContext();
			PortalDataContext dbPortal = new PortalDataContext();
			CategoryNodeDataContext dbCategoryNode = new CategoryNodeDataContext();
			ProductCategoryDataContext dbProductCategory = new ProductCategoryDataContext();
			ProductDataContext dbProduct = new ProductDataContext();

			PortalCatalog portal = new PortalCatalog();

			var thisList = portal.ReadDB(28);



			//	var joined = db.Product
			//		   .Join(db.Shelf, a => a.ShelfIdColumn, b => b.IdColumn, (a, b) => new { a = a, b = b })
			//	   .Join(db.Store, x => x.b.StoreIdColumn, c => c.IdColumn, (x, c) => new { x = x, c = c })
			//.Where(x => x.x.a.IdColumn == 5)
			//.Select(x => x.c.NameColumn);


			var productlist = thisList
					.Join(dbPortalCatalog.ZNodePortalCatalogs, a => a.portalID, b => b.PortalID, (a, b) => new { a = a, b = b })
					.Join(dbCategoryNode.ZNodeCategoryNodes, d => d.b.CatalogID, e => e.CatalogID, (d, e) => new { d = d, e = e })
					.Join(dbProductCategory.ZNodeProductCategories, m => m.e.CategoryID, n => n.CategoryID, (m, n) => new { m = m, n = n })
					.Join(dbProduct.ZNodeProducts, x => x.n.ProductID, y => y.ProductID, (x, y) => new { x = x, y = y })
					.Select
						( row=> new Product
							{
								productID = row.y.ProductID,
								shortDescription = row.y.ShortDescription,
								description = row.y.Description,
								productNumber = row.y.ProductNum,
								imageFile = row.y.ImageFile,
								displayOrder = (!row.y.DisplayOrder.HasValue) ? 0 : (int)row.y.DisplayOrder,
								imageAltTag = row.y.ImageAltTag,
								title = row.y.Name,
								//categoryID = row.x.n.CategoryID,
								//productCategoryID = row.x.n.CategoryID,
                                salePrice = row.y.SalePrice,
								retailPrice = row.y.RetailPrice,
								isOnSale = (row.y.SalePrice.HasValue) ? true : false,
								portalID = (!row.y.PortalID.HasValue) ? 0 : (int)row.y.PortalID

							}
						)
						
						;


			//productlist = productlist.OrderBy(q => q.productCategoryID);
			productlist = productlist
							.Where
								(
									row => row.title.ToLower().Contains(searchtext) 
										//row.description.ToLower().Contains(searchtext) ||
										//row.keywords.ToLower().Contains(searchtext) ||
										//row.shortDescription.ToLower().Contains(searchtext) ||
										//row.SEOKeywords.ToLower().Contains(searchtext) ||
										//row.SEOTitle.ToLower().Contains(searchtext) ||
										//row.SEODescription.ToLower().Contains(searchtext)
                                );

			//dbPortalCatalog.Dispose();
        }

		
	}
}