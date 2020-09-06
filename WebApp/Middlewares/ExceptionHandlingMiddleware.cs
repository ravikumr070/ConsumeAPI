using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace WebApp.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IEmailService emailService,
            IConfiguration config
            )
        {
            _next = next;
            _emailService = emailService;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception).ConfigureAwait(false);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            var msgBuilder = new StringBuilder();
            msgBuilder.AppendLine($"<h1>Message: {exception.Message}</h1>");
            msgBuilder.AppendLine($"Source: {exception.Source}<hr />");
            msgBuilder.AppendLine($"Request Path: {context.Request.Path}<hr />");
            msgBuilder.AppendLine($"QueryString: {context.Request.QueryString}<hr />");
            msgBuilder.AppendLine($"Stack Trace: {exception.StackTrace.Replace(Environment.NewLine, "<br />")}<hr />");
            if (exception.InnerException != null)
                msgBuilder.AppendLine($"Inner Exception<hr />{exception.InnerException?.Message.Replace(Environment.NewLine, "<br />")}<hr />");
            if (context.Request.HasFormContentType && context.Request.Form.Any())
            {
                msgBuilder.AppendLine("<table border=\"1\"><tr><td colspan=\"2\">Form collection:</td></tr>");
                foreach (var (key, value) in context.Request.Form)
                    msgBuilder.AppendLine($"<tr><td>{key}</td><td>{value}</td></tr>");
                msgBuilder.AppendLine("</table>");
            }
            await _emailService.SendEmailAsync(_config["ErrorEmail"], "App Notification", msgBuilder.ToString(), null, null, null, null).ConfigureAwait(false);
            context.Response.Redirect("/Errors/Exception");
        }
    }
}
