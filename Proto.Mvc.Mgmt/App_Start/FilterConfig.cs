using System.Web;
using System.Web.Mvc;

namespace Proto.Mvc.Mgmt
{
    public class FilterConfig
    {
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
        }
    }
}
