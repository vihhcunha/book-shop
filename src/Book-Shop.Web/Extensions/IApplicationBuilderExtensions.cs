using Book_Shop.Web.Middlewares;

namespace Book_Shop.Web.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseCustomErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomErrorHandlerMiddleware>();
        }
    }
}
