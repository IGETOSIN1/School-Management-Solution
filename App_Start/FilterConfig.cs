using System.Web;
using System.Web.Mvc;

namespace WEB_SCONET_MANAGEMENT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
