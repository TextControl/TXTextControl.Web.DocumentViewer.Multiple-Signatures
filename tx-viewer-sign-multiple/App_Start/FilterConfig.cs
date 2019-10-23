using System.Web;
using System.Web.Mvc;

namespace tx_viewer_sign_multiple
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
