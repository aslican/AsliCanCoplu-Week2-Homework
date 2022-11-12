using Owner.API.Middlewares;
using System.Net;
using System.Text.Json;

namespace Owner.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {


            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {


                await HandleExceptionAsync(httpContext, exception);


            }

        }



        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            var stackTrace = String.Empty;
            string message;
            var exceptionType = exception.GetType();

            message = exception.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = exception.StackTrace;


            var exceptionResult = JsonSerializer.Serialize(new
            {
                statusCode = status,
                error = message,
                status = "This is an error message from Middleware.",
                stackTrace,
                

            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(exceptionResult);
        }
    }

}
    static public class ExceptionMiddlewareExtension { 
        static public IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) { 
            return builder.UseMiddleware<ExceptionMiddleware>();
        } 
    }

