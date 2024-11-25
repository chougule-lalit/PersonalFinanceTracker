using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace FinTracker.Api.Controllers
{
    [ApiController]
    [Route("api/documentation")]
    public class ApiDocumentationController : ControllerBase
    {
        private readonly ISwaggerProvider _swaggerProvider;

        public ApiDocumentationController(ISwaggerProvider swaggerProvider)
        {
            _swaggerProvider = swaggerProvider;
        }

        [HttpGet]
        public ActionResult<ApiDocumentation> GetApiDocumentation()
        {
            var swaggerDoc = _swaggerProvider.GetSwagger("v1");
            var documentation = new ApiDocumentation
            {
                Title = swaggerDoc.Info.Title,
                Version = swaggerDoc.Info.Version,
                Endpoints = swaggerDoc.Paths.Select(path => new ApiEndpoint
                {
                    Path = path.Key,
                    Operations = path.Value.Operations.Select(op => new ApiOperation
                    {
                        Method = op.Key.ToString(),
                        Summary = op.Value.Summary,
                        Description = op.Value.Description,
                        Parameters = op.Value.Parameters?.Select(p => new ApiParameter
                        {
                            Name = p.Name,
                            In = p.In.ToString(),
                            Required = p.Required,
                            Schema = ExtractFullSchema(p.Schema, swaggerDoc.Components.Schemas)
                        }).ToList(),
                        RequestBody = op.Value.RequestBody != null ? new ApiSchema
                        {
                            ContentType = op.Value.RequestBody.Content.FirstOrDefault().Key,
                            Required = op.Value.RequestBody.Required,
                            Schema = ExtractFullSchema(op.Value.RequestBody.Content.FirstOrDefault().Value.Schema, swaggerDoc.Components.Schemas)
                        } : null,
                        Responses = op.Value.Responses.Select(r => new ApiResponse
                        {
                            StatusCode = r.Key,
                            Description = r.Value.Description,
                            Schema = r.Value.Content.Any()
                                ? ExtractFullSchema(r.Value.Content.FirstOrDefault().Value.Schema, swaggerDoc.Components.Schemas)
                                : null
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return Ok(documentation);
        }

        private Dictionary<string, object> ExtractFullSchema(OpenApiSchema schema, IDictionary<string, OpenApiSchema> components)
        {
            if (schema == null) return null;

            var result = new Dictionary<string, object>();

            // If it's a reference, resolve it
            if (schema.Reference != null)
            {
                var refKey = schema.Reference.Id;
                if (components.ContainsKey(refKey))
                {
                    var resolvedSchema = components[refKey];
                    return ExtractFullSchema(resolvedSchema, components);
                }
            }

            if (schema.Type != null)
                result["type"] = schema.Type;

            if (schema.Format != null)
                result["format"] = schema.Format;

            if (schema.Properties != null)
            {
                var properties = new Dictionary<string, Dictionary<string, object>>();
                foreach (var prop in schema.Properties)
                {
                    properties[prop.Key] = ExtractFullSchema(prop.Value, components);

                    // Include property type information
                    if (prop.Value.Type != null)
                        properties[prop.Key]["type"] = prop.Value.Type;

                    // Include nullability
                    properties[prop.Key]["nullable"] = prop.Value.Nullable;

                    // Include enum values if present
                    if (prop.Value.Enum?.Any() == true)
                        properties[prop.Key]["enum"] = prop.Value.Enum;
                }
                result["properties"] = properties;
            }

            if (schema.Required?.Any() == true)
                result["required"] = schema.Required;

            if (schema.Items != null)
            {
                result["type"] = "array";
                result["items"] = ExtractFullSchema(schema.Items, components);
            }

            result["nullable"] = schema.Nullable;

            if (schema.Example != null)
                result["example"] = schema.Example;

            if (schema.Default != null)
                result["default"] = schema.Default;

            if (schema.Enum?.Any() == true)
                result["enum"] = schema.Enum;

            return result;
        }
    }

    public class ApiDocumentation
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public List<ApiEndpoint> Endpoints { get; set; }
    }

    public class ApiEndpoint
    {
        public string Path { get; set; }
        public List<ApiOperation> Operations { get; set; }
    }

    public class ApiOperation
    {
        public string Method { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public List<ApiParameter> Parameters { get; set; }
        public ApiSchema RequestBody { get; set; }
        public List<ApiResponse> Responses { get; set; }
    }

    public class ApiParameter
    {
        public string Name { get; set; }
        public string In { get; set; }
        public bool Required { get; set; }
        public Dictionary<string, object> Schema { get; set; }
    }

    public class ApiSchema
    {
        public string ContentType { get; set; }
        public bool Required { get; set; }
        public Dictionary<string, object> Schema { get; set; }
    }

    public class ApiResponse
    {
        public string StatusCode { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> Schema { get; set; }
    }

}
