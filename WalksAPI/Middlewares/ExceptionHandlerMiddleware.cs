using System.Net;

namespace WalksAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        public ILogger<ExceptionHandlerMiddleware> Logger { get; }
        public RequestDelegate Request { get; }
        private static int Id { get; set; } = 1;


        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, 
            RequestDelegate request)
        {
            Logger = logger;
            Request = request;
        }


        public async void InvokeAsync (HttpContext httpContext)
        {
            try
            {
                await Request(httpContext);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{Id} : {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType= "application/json";

                var error = new
                {
                    id = Id,
                    message = "Something Went Wrong!",
                };

                throw;
            }
        }
    }
}
