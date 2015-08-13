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


		public IEnumerable<Product> ProductList { get; set; }
		public int PortalID { get; set; }
		public int Count { get; set; }

		public Boolean Keywords(string searchtext, string fieldname, string mode)
		{
			//TODO: DEBUG_DAO create a function to build these filters. using brute force right now
			// need to add quoted phrases, exclude operators, whole or partial wor support
			// algorithm not always returning values, or sometimes rturns multiple when there should be one


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

		
			// set operational modes
			//fieldName = all | title | description    mode = all | exact | any

			if ((fieldname != null) && (fieldname.Length > 0))
			{
				fieldname = fieldname.ToLower();
			}
			else
			{
				fieldname = null;
			}

			if ((mode != null) && (mode.Length > 0))
			{
				mode = mode.ToLower();
			}
			else
			{
				mode = null;
			}


			// get any search terms

			String[] searchTerms = new String[] { };
			string[] separators = { ",", ".", "!", "?", ";", ":", " " };

			if (mode!=null)
			{
				mode = mode.ToLower();
				if (mode == "all" || mode == "any")
				{
					searchTerms = searchtext.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				}
				else
				{
					searchTerms = null; // use searctext for an exact phrase matching 
                }
			}


			if (fieldname == "all" || fieldname == null)
			{
				if (mode == "exact")
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
				else if ((mode == "any") || (mode == null))
				{
					productlist = productlist
						.Where
							(
							row => searchTerms.Any
								(
									// using most common fields now
									term => row.title.ToLower().Contains(term) ||
									row.description.ToLower().Contains(term) ||
									row.keywords.ToLower().Contains(term)
								//row.shortDescription.ToLower().Contains(term) ||
								//row.SEOKeywords.ToLower().Contains(term) ||
								//row.SEOTitle.ToLower().Contains(term) ||
								//row.SEODescription.ToLower().Contains(term)
								)
						);


				}
				else if (mode == "all")
				{
					productlist = productlist
						.Where
							(
							row => searchTerms.All
								(
									// using most common fields now
									term => row.title.ToLower().Contains(term) ||
									row.description.ToLower().Contains(term) ||
									row.keywords.ToLower().Contains(term)
								//row.shortDescription.ToLower().Contains(term) ||
								//row.SEOKeywords.ToLower().Contains(term) ||
								//row.SEOTitle.ToLower().Contains(term) ||
								//row.SEODescription.ToLower().Contains(term)
								)
						);


				}
			}
			else if (fieldname == "title")
			{
				if (mode == "exact")
				{
					productlist = productlist
										.Where
											(
											row => row.title.ToLower().Contains(searchtext)
											);
				}
				else if ((mode == "any") || (mode == null))
				{
					productlist = productlist
						.Where
							(
							row => searchTerms.Any
								(
									term => row.title.ToLower().Contains(term) 
								)
						);
				}
				else if (mode == "all")
				{
					productlist = productlist
						.Where
							(
							row => searchTerms.All
								(
									term => row.title.ToLower().Contains(term)
								)
						);
				}
			}
			else if (fieldname == "description")
			{
				if (mode == "exact")
				{
					productlist = productlist
										.Where
											(
											row => row.description.ToLower().Contains(searchtext)
											);
				}
				else if ((mode == "any") || (mode == null))
				{
					productlist = productlist
						.Where
							(
							row => searchTerms.Any
								(
									term => row.description.ToLower().Contains(term)
								)
						);
				}
				else if (mode == "all")
				{
					productlist = productlist
						.Where
							(
							row => searchTerms.All
								(
									term => row.description.ToLower().Contains(term)
								)
						);
				}

			}
			else if (fieldname == "keywords")
			{
				if (mode == "exact")
				{
					productlist = productlist
										.Where
											(
											row => row.keywords.ToLower().Contains(searchtext)
											);
				}
				else if ((mode == "any") || (mode == null))
				{
					productlist = productlist
						.Where
							(
							row => searchTerms.Any
								(
									term => row.keywords.ToLower().Contains(term)
								)
						);
				}
				else if (mode == "all")
				{
					productlist = productlist
						.Where
							(
							row => searchTerms.All
								(
									term => row.keywords.ToLower().Contains(term)
								)
						);
				}

			}


		

			productlist = productlist
							.OrderBy(row => row.productCategoryID)
							.Take(100);

	
		
			if (productlist != null)
			{
				try {
					fStatus = true;
					Count = productlist.Count();
				}
				catch (Exception e)
				{
					Count = 0;
					productlist = null;
                }
            }

		
			return fStatus;
        }

		
	}
}