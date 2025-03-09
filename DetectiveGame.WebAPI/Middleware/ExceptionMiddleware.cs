using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DetectiveGame.WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorResponse();

            switch (exception)
            {
                case ValidationException validationEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Validation error";
                    response.Errors = validationEx.Errors.Select(e => e.ErrorMessage).ToList();
                    break;

                case NotFoundException notFoundEx:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = notFoundEx.Message;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "An error occurred while processing your request.";
                    break;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
} 