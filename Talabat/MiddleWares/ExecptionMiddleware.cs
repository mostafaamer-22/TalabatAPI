﻿using System.Net;
using System.Text.Json;
using Talabat.Errors;

namespace Talabat.MiddleWares
{
    public class ExecptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExecptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExecptionMiddleware(RequestDelegate next 
            , ILogger<ExecptionMiddleware> logger ,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next(context);

            }catch(Exception ex)
            {
                _logger.LogError(ex , ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _environment.IsDevelopment() ?
                    new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()):
                    new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy  =  JsonNamingPolicy.CamelCase,
                };

                var json = JsonSerializer.Serialize(response , options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
