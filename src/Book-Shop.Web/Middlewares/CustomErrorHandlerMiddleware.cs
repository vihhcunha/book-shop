using Sentry;

namespace Book_Shop.Web.Middlewares;

public class CustomErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HandleError(ex);
            throw;
        }
    }

    private void HandleError(Exception ex)
    {
        SentrySdk.CaptureException(ex);
    }
}