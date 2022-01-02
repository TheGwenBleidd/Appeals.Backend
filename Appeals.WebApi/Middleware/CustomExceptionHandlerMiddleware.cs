using Appeals.Application.Common.Exceptions;
using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace Appeals.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                throw;
            }   
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (ex) 
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = Convert.ToInt32(code);

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { exceptionMessage = ex.Message });
            }
            
            return context.Response.WriteAsync(result);
        }
    }
}
