using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace He.Framework.Extension.Consul
{
    /// <summary>
    /// 心跳检查
    /// </summary>
    public static class HealthCheckMiddleware
    {
        /// <summary>
        /// 设置心跳检查
        /// </summary>
        /// <param name="app"></param>
        /// <param name="checkpath">检查路径<para>示例值：/healthcheck</para></param>
        /// <returns></returns>
        public static void UseHealthCheckMiddle(this IApplicationBuilder app, string checkpath = "/healthcheck")
        {
            app.Map(checkpath, applicationBuilder => applicationBuilder.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync("OK");
            }));
        }
    }
}
