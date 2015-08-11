using System;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using store_api_test.DataMapping;


namespace store_api_test.Models
{
	public class Search
	{
		private Product product = new Product();


		public IEnumerable<Product> ProductList { get; set; }
		public int PortalID { get; set; }
		public int Count { get; set; }

		public Boolean Keywords(string searchtext, string fieldname, string mode)
		{
			PortalCatalogDataContext dbPortalCatalog = new PortalCatalogDataContext();
			PortalDataContext dbPortal = new PortalDataContext();
			CategoryNodeDataContext dbCategoryNode = new CategoryNodeDataContext();
			ProductCategoryDataContext dbProductCategory = new ProductCategoryDataContext();
			ProductDataContext dbProduct = new ProductDataContext();

			PortalCatalog portal = new PortalCatalog();

			Boolean fStatus = false;

			// Work through the database  to find all products associated with a portal ID.
			// Znode has a few tables that simply act as relationship tables, hence the complexity of this query.

			var listPortalCatalog = portal.ReadDB(PortalID);

			var productlist = listPortalCatalog.AsQueryable()

					// staring with the portal ID, grab all catalogs for that portal
					.Join
						(dbPortalCatalog.ZNodePortalCatalogs,
							tempPortalCatalogs => tempPortalCatalogs.portalID,
							tempPortalCategories => tempPortalCategories.PortalID,
								(tempPortalCatalogs, tempPortalCategories) => new
								{ tempPortalCatalogs = tempPortalCatalogs, tempPortalCategories = tempPortalCategories }
						) // produces joinedPortalCatalogs

					// get mapping of catalogs to various categories
					.Join
						(dbCategoryNode.ZNodeCategoryNodes,
							joinedPortalCatalogs => joinedPortalCatalogs.tempPortalCategories.CatalogID,
							tempCategoryNode => tempCategoryNode.CatalogID,
								(joinedPortalCatalogs, tempCategoryNode) => new
								{ joinedPortalCatalogs = joinedPortalCatalogs, tempCategoryNode = tempCategoryNode }
						) // produces joinedCategoryNode

					// get mapping of all product IDs within categories
					.Join
						(dbProductCategory.ZNodeProductCategories,
							joinedCategoryNode => joinedCategoryNode.tempCategoryNode.CategoryID,
							tempProductCategories => tempProductCategories.CategoryID,
								(joinedCategoryNode, tempProductCategories) => new
								{ joinedCategoryNode = joinedCategoryNode, tempProductCategories = tempProductCategories }
						) // produces joinedProductCategories

					// get mapping of all products to categories and get product details
					.Join
						(dbProduct.ZNodeProducts,
							joinedProductCategories => joinedProductCategories.tempProductCategories.ProductID,
							tempProducts => tempProducts.ProductID,
								(joinedProductCategories, tempProducts) => new
								{ joinedProductCategories = joinedProductCategories, tempProducts = tempProducts }
						)  // produces final product list (tempProducts)

					.Select
						(row => new Product
						{
							productID = row.tempProducts.ProductID,
								shortDescription = row.tempProducts.ShortDescription,
								description = row.tempProducts.Description,
								productNumber = row.tempProducts.ProductNum,
								imageFile = row.tempProducts.ImageFile,
								displayOrder = (!row.tempProducts.DisplayOrder.HasValue) ? 0 : (int)row.tempProducts.DisplayOrder,
								imageAltTag = row.tempProducts.ImageAltTag,
								title = row.tempProducts.Name,
								salePrice = row.tempProducts.SalePrice,
								retailPrice = row.tempProducts.RetailPrice,
								isOnSale = (row.tempProducts.SalePrice.HasValue) ? true : false,
								portalID = (!row.tempProducts.PortalID.HasValue) ? 0 : (int)row.tempProducts.PortalID

							}
						);

			//TODO: DEBUG_DAO create a function to build these filters. using brute force right now

			//	productlist = productlist.Where("");

			String[] sWords = new String[] { };
			string[] separators = { ",", ".", "!", "?", ";", ":", " " };

			if (mode!=null)
			{
				mode = mode.ToLower();
				if (mode == "all" || mode == "any")
				{
					sWords = searchtext.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				}

			}

			if (fieldname != null)
			{
				fieldname = fieldname.ToLower();
				if (fieldname == "all")
				{
					productlist = productlist
									.Where
										(
										row => row.title.ToLower().Contains(searchtext) ||
										row.description.ToLower().Contains(searchtext) ||
										row.keywords.ToLower().Contains(searchtext) ||
										row.shortDescription.ToLower().Contains(searchtext) ||
										row.SEOKeywords.ToLower().Contains(searchtext) ||
										row.SEOTitle.ToLower().Contains(searchtext) ||
										row.SEODescription.ToLower().Contains(searchtext)
										);

				}
				else if (fieldname == "title")
				{
					productlist = productlist
									.Where
										(
										row => row.title.ToLower().Contains(searchtext)
										);

				}
				else if (fieldname == "description")
				{
					productlist = productlist
									.Where
										(
										row => row.description.ToLower().Contains(searchtext)
										);

				}
				else if (fieldname == "keyword")
				{
					productlist = productlist
									.Where
										(
										row => row.keywords.ToLower().Contains(searchtext)
										);

				}
			}
			else
			{
				if (mode == "any")
				{
					productlist = productlist
								.Where (row => row.title.ToLower().Contains("") ||
									row.description.ToLower().Contains(searchtext) ||
									row.keywords.ToLower().Contains(searchtext) ||
									row.shortDescription.ToLower().Contains(searchtext)
									);
				}
				else if (mode=="all")
				{
					String sQ = null;

					for (int x =0; x < sWords.Length; x++)
					{
						sQ += "( title like '%" + sWords[x] + "%' ) ";
						if (x < sWords.Length - 1)
						{
							sQ += " AND ";
						}
                    }

					productlist = productlist.Where(sQ);
				}
				else {
					// exact match of phrase
					productlist = productlist
									.Where
										(
										row => row.title.ToLower().Contains(searchtext) ||
										row.description.ToLower().Contains(searchtext) ||
										row.keywords.ToLower().Contains(searchtext) ||
										row.shortDescription.ToLower().Contains(searchtext)
										);
				}
			}


			productlist = productlist
							.OrderBy(row => row.productCategoryID);

			//dbPortalCatalog.Dispose();
			ProductList = productlist;

			if (productlist != null)
			{
				fStatus = true;
				Count = productlist.Count();
            }

		
			return fStatus;
        }

		
	}
}