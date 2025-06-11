using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeVote.Models
{
    // Filter to clear string defaults in OpenApi schema, used in program.cs under AddSwaggerGen
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
