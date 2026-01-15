using FreKE.Application.Features.Logs;
using FreKE.Application.Features.Users.DTOs;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace FreKE.API.Middlewares
{
    public class CustomLoggingMiddleware : IMiddleware
    {
        private readonly ILogRepository _logRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomLoggingMiddleware( ILogRepository logRepository, IHttpContextAccessor httpContextAccessor)
        {
            _logRepository = logRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.EnableBuffering();
            var logRequest = new RequestLogDto();
            var log = new Log();
            logRequest.Query = context.Request.QueryString.ToString();

            // Request Body reader
            logRequest.Body = await ReadRequestBodyAsync(context.Request);
            
            // Has Token
            if (context.User.Identity?.IsAuthenticated == true)
            {
                log.UserId = Guid.Parse(context.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier.ToString()).FirstOrDefault()!.Value);
            }

            log.LogLevel = LogLevel.Information.ToString();
            log.Request = JsonConvert.SerializeObject(logRequest);
            log.Endpoint = context.Request.Path;
            log.ActionMethod = context.Request.Method;
            await _logRepository.AddAsync(log);
            await next(context);
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            return body;
        }
    }
}
