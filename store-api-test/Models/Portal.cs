using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace store_api_test.Models
{
	public class Portal
	{
		public bool isActive { get; set; }
		public string storeName { get; set; }
		public string companyName { get; set; }
		public int portalID { get; set; }
	}
}