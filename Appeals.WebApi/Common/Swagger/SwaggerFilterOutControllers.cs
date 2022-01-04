using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Appeals.WebApi.Common.Swagger
{
    public class SwaggerFilterOutControllers : IDocumentFilter
    {
        private readonly string _environment;
        public SwaggerFilterOutControllers(string environment)
        {
            _environment = environment.ToUpper();
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (_environment == "LOCAL" || _environment == "DEVELOPMENT")
                return;

            var allowPaths = new List<string>() { "/Test" };
            List<string> pathKeysToRemove = swaggerDoc.Paths.Where(x => !allowPaths.Any(e => x.Key.Contains(e))).Select(x => x.Key).ToList();
            foreach (var pathKey in pathKeysToRemove)
            {
                swaggerDoc.Paths.Remove(pathKey);
            }
        }
    }
}
