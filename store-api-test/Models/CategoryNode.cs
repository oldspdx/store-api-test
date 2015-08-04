using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using store_api_test.DataMapping;

namespace store_api_test.Models
{
    public class CategoryNode
    {
        public int categoryNodeID { get; set; }
		public int catalogID { get; set; }
		public int categoryID { get; set; }
		public int? parentCategoryNodeID { get; set; }
        public int displayOrder { get; set; }
        public int portalID { get; set; }

        public bool subCategoryGridVisibleInd { get; set; }
		public bool isActive { get; set; }

		public DateTime? dateStart { get; set; }
		public DateTime? dateEnd { get; set; }

        public string theme { get; set; }
        public string masterPage { get; set; }
        public string CSS { get; set; }
		public string title { get; set; }
		public string shortDescription { get; set; }
		public string imageFile { get; set; }
		public string description { get; set; }

		public IEnumerable<CategoryNode> ReadDB(int? catalogID, int? parentID)
		{
			IEnumerable<CategoryNode> iCatNode;
			CategoryNodeDataContext db = new CategoryNodeDataContext();
			CategoryDataContext dbCategory = new CategoryDataContext();
		

			iCatNode = db.ZNodeCategoryNodes.AsEnumerable()
				.Join
					(dbCategory.ZNodeCategories,
						_CatNode => _CatNode.CategoryID,
						_Category => _Category.CategoryID,

					(_CatNode, _Category) => new
						{
							_Category.Name,
							_Category.PortalID,
							_Category.ShortDescription,
							_Category.Title,
							_Category.ImageFile,
							_Category.Description,
							_CatNode.ActiveInd,
							_CatNode.CatalogID,
							_CatNode.CategoryID,
							_CatNode.CategoryNodeID,
							_CatNode.DisplayOrder,
							_CatNode.BeginDate,
							_CatNode.EndDate,
							_CatNode.MasterPage,
							_CatNode.Theme,
							_CatNode.CSS,
							_CatNode.ParentCategoryNodeID
						}
					)
				.Select 
					(row => new CategoryNode
						{
							catalogID = (!row.CatalogID.HasValue) ? 0 : (int)row.CatalogID,
							isActive = row.ActiveInd,
							theme = row.Theme,
							masterPage = row.MasterPage,
							dateStart = row.BeginDate,
							dateEnd = row.EndDate,
							CSS = row.CSS,
							title = row.Title,
							categoryNodeID = row.CategoryNodeID,
							categoryID = row.CategoryID,
							parentCategoryNodeID = row.ParentCategoryNodeID,
							displayOrder = (!row.DisplayOrder.HasValue) ? 0 : (int)row.DisplayOrder
						}
					);

			if (catalogID.HasValue)
			{
				iCatNode = iCatNode
					.OrderBy ( x => x.displayOrder)
					.ThenBy ( x => x.title)
					.Where ( row => row.catalogID == catalogID );
			}

			if (parentID.HasValue)
			{
				iCatNode = iCatNode
					.OrderBy(x => x.displayOrder)
					.ThenBy(x => x.title)
					.Where(row => row.parentCategoryNodeID == parentID);
			}

			return iCatNode;
		}

        
    }
}