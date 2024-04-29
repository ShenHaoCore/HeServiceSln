using He.Framework.Attribute;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace He.Framework.Extension.Swagger
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerApiFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool isUploadApi = context.ApiDescription.CustomAttributes().Any(it => it is UploadApiAttribute);
            if (isUploadApi) { operation.Parameters.Add(new OpenApiParameter { Name = "File", In = ParameterLocation.Header, Description = "文件上传", Required = true, Schema = new OpenApiSchema { Type = "file" } }); }
        }
    }
}
