using Microsoft.AspNetCore.Http;

namespace He.Framework.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestIdMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public RequestIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Append("X-Request-Id", Guid.NewGuid().ToString()); // 添加唯一标识到响应头中
            await _next(context); // 调用下一个中间件或者终结请求管道
        }
    }
}
