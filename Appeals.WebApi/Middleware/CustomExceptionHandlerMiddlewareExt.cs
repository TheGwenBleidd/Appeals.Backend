namespace Appeals.WebApi.Middleware
{
    public static class CustomExceptionHandlerMiddlewareExt
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
