using System.Web;
using System.Web.Mvc;

namespace MvcWeb
{
    public class FilterConfig
    {
        // Add the Authorize filter and the RequireHttps filter to the application. 
        // An alternative approach is to add the Authorize attribute and the 
        // RequireHttps attribute to each controller, but it's considered 
        // a security best practice to apply them to the entire application. 
        // By adding them globally, every new controller and action method 
        // you add will automatically be protected, you won't need to remember to apply them. 
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // http://stackoverflow.com/questions/6508415/application-error-not-firing-when-customerrors-on
            // By default (when a new project is generated), an MVC application has some logic 
            // in the Glabal.asax.cs file. This logic is used for mapping routes and registering filters. 
            // By default, it only registers one filter: a HandleErrorAttribute filter. When customErrors are on 
            // (or through remote requests when it is set to RemoteOnly), the HandleErrorAttribute tells MVC to 
            // look for an Error view and it never calls the Application_Error() method. I couldn't find 
            // documentation of this but it is explained in this answer on programmers.stackexchange.com here:
            // http://programmers.stackexchange.com/questions/45195/what-are-the-definitive-guidelines-for-custom-error-handling-in-asp-net-mvc-3/45197#45197

            // To get the ApplicationError() method called for every unhandled exception, 
            // simple remove the line which registers the HandleErrorAttribute filter.

            filters.Add(new HandleErrorAttribute());
            // http://www.windowsazure.com/en-us/develop/net/tutorials/web-site-with-sql-database/
            // The Authorize filter will prevent anonymous users from accessing any methods in the application. 
            // You will use the AllowAnonymous attribute to opt out of the authorization requirement in a 
            // couple methods, so anonymous users can log in and can view the home page. 
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            // The RequireHttps will require all access to the web app be through HTTPS.
            filters.Add(new RequireHttpsAttribute());
        }

        //public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //}
    }
}
