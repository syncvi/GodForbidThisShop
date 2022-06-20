using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System;

namespace ShoppingAttire.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ImageMiddleware
    {
        private readonly RequestDelegate _next;

        public ImageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var files = Directory.GetFiles("./Pics");
            string url = httpContext.Request.Path;
            var rand = new Random();
            var randIndex = rand.Next(0, files.Length);
            if (url.ToLower().Contains(".jpg"))
            {
                Image img = Image.FromFile(files[randIndex]);
                MemoryStream stream = new();
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png); //saving in bytes to send anywhere
                httpContext.Response.ContentType = "image/jpeg"; //tells front that it's a pic and what type
                return httpContext.Response.Body.WriteAsync(stream.ToArray(), 0,
                (int)stream.Length); //adding the bytes (the picture itself) to the response's body
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ImageMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageMiddleware>();
        }
    }
}
