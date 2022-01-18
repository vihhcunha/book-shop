using Microsoft.AspNetCore.Mvc.Razor;

namespace Book_Shop.Web.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatDoc(this RazorPage page, int personKind, string doc)
        {
            return personKind == 1 ? Convert.ToUInt64(doc).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(doc).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
