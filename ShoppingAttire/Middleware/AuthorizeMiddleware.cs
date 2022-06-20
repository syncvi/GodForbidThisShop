using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ShoppingAttire.Middleware
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        //w tym przypadku middleware umozliwia aplikacji na porcie 4200, ktora rowniez korzysta z localhosta, na uzywanie endpointow (zapytan) aplikacji (backendu)
        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = 200;
                context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "http://localhost:5001", "http://localhost:4200" }); //port na ktorym angular domyslnie tworzy aplikacje
                context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Content-Type,cache-control,x-microsoftajax,x-requested-with" });
                context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET,POST,PUT,DELETE,PATCH" });
                context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                return;
            }



            await _next.Invoke(context);
        }
    }
}
