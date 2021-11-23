using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiDemo.Web.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IHostingEnvironment hostingEnvironment)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                Log(httpContext, e, hostingEnvironment);
                throw;
            }
        }

        private void Log(HttpContext context, Exception exception, IHostingEnvironment hostingEnvironment)
        {
            string savePath = String.Format("{0}/{1}", hostingEnvironment.WebRootPath, "exceptionHandlingLog/");
            DateTime currentDate = DateTime.UtcNow;
            string fileName = $"{currentDate.ToString("yyyy-MM-dd")}_log";
            var filePath = Path.Combine(savePath, fileName);

            // ensure that directory exists
            new FileInfo(filePath).Directory.Create();

            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine($"{currentDate.ToString("HH:mm:ss")} {context.Request.Path}");
                writer.WriteLine(exception.Message);
            }
        }
    }
}
