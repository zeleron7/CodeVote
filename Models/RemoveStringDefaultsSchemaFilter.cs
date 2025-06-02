using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeVote.Models
{
    public class ClearStringExamplesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            foreach (var prop in schema.Properties)
            {
                if (prop.Value.Type == "string")
                {
                    prop.Value.Example = new OpenApiString("");
                }
            }
        }
    }
}
