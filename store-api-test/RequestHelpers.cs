using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace store_api_test
{
	public static class RequestHelpers
	{
		// get all querystring values and place into a dictionary object
		// TODO: this only finds one querystring parameter of a given name. 
		// Can refactor to find all values of a given querystring parameter
		// if used multiple times.

		public static Dictionary<string, string> GetQueryStrings (this HttpRequestMessage request)
		{
			return request.GetQueryNameValuePairs()
						  .ToDictionary ( kv => kv.Key, 
										  kv => kv.Value, 
										  StringComparer.OrdinalIgnoreCase);
		}


		// gets a single querystring value (again, only the first one)
		public static string GetQueryString (this HttpRequestMessage request, string key)
		{
			var queryStrings = request.GetQueryNameValuePairs();

			if (queryStrings == null)
				return null;

			// find first match
			var match = queryStrings.FirstOrDefault ( kv => string.Compare (kv.Key, key, true) == 0 );

			if (string.IsNullOrEmpty(match.Value))
				return null;

			return match.Value;
		}


		public static string GetCookie (this HttpRequestMessage request, string cookieName)
		{
			CookieHeaderValue cookie = request.Headers.GetCookies(cookieName).FirstOrDefault();

			if (cookie != null)
				return cookie[cookieName].Value;

			return null;
		}



		//public static string GetHeader TBD
		// TODO


	}
}