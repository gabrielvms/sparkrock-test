﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Text.Json;

namespace Common.Helpers
{
    public static class ErrorHandler
    {
        /// <summary>
        /// Global unhandled exception handler.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="environment"></param>
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }

        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: true);

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: false);

        private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            // Try and retrieve the error from the ExceptionHandler middleware
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;

            // Should always exist, but best to be safe!
            if (ex != null)
            {
                // ProblemDetails has it's own content type
                httpContext.Response.ContentType = "application/problem+json";

                // Get the details to display, depending on whether we want to expose the raw exception
                var title = includeDetails ? "An error occured: " + ex.Message : "An error occured";
                var details = includeDetails ? ex.ToString() : null;

                var problem = new ProblemDetails
                {
                    Status = 500,
                    Title = title,
                    Detail = details
                };

                // This is often very handy information for tracing the specific request
                var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
                if (traceId != null)
                {
                    problem.Extensions["traceId"] = traceId;
                }

                //Serialize the problem details object to the Response as JSON (using System.Text.Json)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var stream = httpContext.Response.Body;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                await JsonSerializer.SerializeAsync(stream, problem);
            }
        }
    }
}
