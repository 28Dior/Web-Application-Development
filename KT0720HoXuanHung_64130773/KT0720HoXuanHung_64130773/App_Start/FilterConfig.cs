using System.Web;
using System.Web.Mvc;

namespace KT0720HoXuanHung_64130773
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
