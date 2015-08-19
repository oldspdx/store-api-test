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
			CategoryDataContext dbCategories = new CategoryDataContext();

			PortalCatalog portal = new PortalCatalog();

			Boolean fStatus = false;

			// Work through the database  to find all products associated with a portal ID.
			// Znode has a few tables that simply act as relationship tables, hence the complexity of this query.

			var listPortalCatalog = portal.ReadDB(PortalID);

			var _productlist = listPortalCatalog.AsEnumerable()

					// staring with the portal ID, grab all catalogs for that portal
				
					// get mapping of catalogs to various categories
					.Join
						(dbCategoryNode.ZNodeCategoryNodes,
							joinedPortalCatalogs => joinedPortalCatalogs.catalogID,
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
					.Distinct()
					.Select
						(row => new Product
							{
								productID = row.tempProducts.ProductID,
								parentCategoryID = row.joinedProductCategories.tempProductCategories.CategoryID,
								categoryID = row.joinedProductCategories.joinedCategoryNode.tempCategoryNode.CategoryID,
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

			if (mode != null)
			{
				mode = mode.ToLower();
			}
				if (mode == "all" || mode == "any" || mode==null)
				{
					searchTerms = searchtext.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				}
				else
				{
					searchTerms = null; // use searctext for an exact phrase matching 
                }
			


			if (fieldname == "all" || fieldname == null)
			{
				if (mode == "exact")
				{
					_productlist = _productlist
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
					var p1 = _productlist
					.Where
					(
							row => searchTerms.Any
							(
								term => row.title.ToLower().Contains(term)
							)
					);
					var p2 = _productlist
							.Where
						(
								row => searchTerms.Any
								(
									term => row.description.ToLower().Contains(term)
								)
						);
					var p3 = _productlist
						.Where
						(
								row => searchTerms.Any
								(
									term => row.keywords.ToLower().Contains(term)
								)
						);

					_productlist = p1.Union(p2).ToList();


				}
				else if (mode == "all")
				{

					var p1 = _productlist
							.Where
							(
									row => searchTerms.All
									(
										term => row.title.ToLower().Contains(term)
									)
							);
					var p2 = _productlist
							.Where
						(
								row => searchTerms.All
								(
									term => row.description.ToLower().Contains(term)
								)
						);
                    var p3 = _productlist
						.Where
						(
								row => searchTerms.All
								(
									term => row.keywords.ToLower().Contains(term)
								)
						);

					_productlist = p1.Union(p2).ToList();
					

					//_productlist.Union(p3);


				}
			}
			else if (fieldname == "title")
			{
				if (mode == "exact")
				{
					_productlist = _productlist
										.Where
											(
											row => row.title.ToLower().Contains(searchtext)
											);
				}
				else if ((mode == "any") || (mode == null))
				{
					_productlist = _productlist
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
					_productlist = _productlist
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
					_productlist = _productlist
										.Where
											(
											row => row.description.ToLower().Contains(searchtext)
											);
				}
				else if ((mode == "any") || (mode == null))
				{
					_productlist = _productlist
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
					_productlist = _productlist
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
					_productlist = _productlist
										.Where
											(
											row => row.keywords.ToLower().Contains(searchtext)
											);
				}
				else if ((mode == "any") || (mode == null))
				{
					_productlist = _productlist
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
					_productlist = _productlist
						.Where
							(
							row => searchTerms.All
								(
									term => row.keywords.ToLower().Contains(term)
								)
						);
				}

			}




			_productlist = _productlist
							.OrderBy(row => row.productCategoryID);


	
		
			if (_productlist != null)
			{
				try {
					fStatus = true;
					Count = _productlist.Count();
					ProductList = _productlist;
				}
				catch (Exception e)
				{
					Count = 0;
					ProductList = null;
                }
            }

		
			return fStatus;
        }

		
	}
}