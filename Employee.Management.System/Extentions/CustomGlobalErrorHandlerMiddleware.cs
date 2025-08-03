using Employee.Management.System.middleware;

namespace Employee.Management.System.Extentions
{
    public static class CustomGlobalErrorHandlerMiddleware
    {
        public static IApplicationBuilder GlobalErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandling>();
        }
    }
}
