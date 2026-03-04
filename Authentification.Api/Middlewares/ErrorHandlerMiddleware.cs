using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Responses;

namespace Application.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        public readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next) 
        { 
            _next = next; 
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex) 
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            string message;
            HttpStatusCode statusCode;
            Guid? guid = Guid.NewGuid();
            Log.Error(ex, $"Exception Guid: {guid}");
            switch(ex)
            {
                case ArgumentException _ :
                    statusCode = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;

                case CustomException _ :
                    guid = null;
                    statusCode = HttpStatusCode.PreconditionFailed;
                    message = ex.Message;
                    break;

                default: 
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Unexpected error found.";
                    break;
            }
            string response = JsonSerializer.Serialize(guid.HasValue ? new ErrorResponse(message, guid.Value) : new ErrorResponse(message));
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsync(response);
        }
    }
}
