using System.Web;
using System.Web.Mvc;

namespace BeyondThemes.BeyondAdmin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
