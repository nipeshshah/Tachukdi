using System.Web.Http;
using System.Web.Http.Cors;

namespace Tachukdi
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services
      //config.SuppressDefaultHostAuthentication();
      //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
      var cors = new EnableCorsAttribute("*", "*", "*");
      config.EnableCors(cors);
      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{action}",
          defaults: new { id = RouteParameter.Optional }
      );

      //config.Routes.MapHttpRoute(  
      //    name: "DefaultApi",  
      //    routeTemplate: "api/{controller}/{action}/{id}",  
      //    defaults: new { id = RouteParameter.Optional }  
      //);   
      config.Formatters.Clear();
      config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());

    }
  }
}
