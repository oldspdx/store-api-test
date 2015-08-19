using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace store_api_test
{
	public class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
		

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "Portals",
				routeTemplate: "api/{controller}/storename/{name}",
				defaults: new
				{
					controller = "GetPortalByName",
					name = RouteParameter.Optional
				});


			config.Routes.MapHttpRoute(
				name: "Subcategory",
				routeTemplate: "api/categorynode/{id}/parent/{parent}",
				defaults: new
				{
					controller = "CategoryNode",
					name = RouteParameter.Optional
				});


			config.Routes.MapHttpRoute(
				name: "ProducyByCategory",
				routeTemplate: "api/product/category/{categoryID}",
				defaults: new
				{
					controller = "Product",
					name = RouteParameter.Optional
				});


			//optional params of fieldName = all | title | description  mode = all | exact | any
            config.Routes.MapHttpRoute(
				name: "ProducyByKeyword",
				routeTemplate: "api/product/portal/{portalID}/keyword/{textstring}/{fieldname}/{mode}",
				defaults: new
				{
					controller = "Product",
					textstring = RouteParameter.Optional,
					fieldname = RouteParameter.Optional,
					mode = RouteParameter.Optional
				});


			config.Routes.MapHttpRoute(
				name: "ProducyBySKU",
				routeTemplate: "api/product/{portalID}/sku/{textstring}",
				defaults: new
				{
					controller = "Product",
					textstring = RouteParameter.Optional
				});

			config.Routes.MapHttpRoute(
				name: "ProducyByBrand",
				routeTemplate: "api/product/{portalID}/brand/{textstring}",
				defaults: new
				{
					controller = "Product",
					textstring = RouteParameter.Optional
				});


			config.Routes.MapHttpRoute(
				name: "ProducyByType",
				routeTemplate: "api/product/{portalID}/type/{textstring}",
				defaults: new
				{
					controller = "Product",
					textstring = RouteParameter.Optional
				});


			config.Routes.MapHttpRoute(
				name: "ProductByPrice",
				routeTemplate: "api/product/{portalID}/price/{textstring}",
				defaults: new
				{
					controller = "Product",
					name = RouteParameter.Optional
				});


			// optional params of model={string}  year={string}   make={string}
			config.Routes.MapHttpRoute(
				name: "ProductByFilter",
				routeTemplate: "api/product/{portalID}/filter{filters}",
				defaults: new
				{
					controller = "Product",
					name = RouteParameter.Optional
				});

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}