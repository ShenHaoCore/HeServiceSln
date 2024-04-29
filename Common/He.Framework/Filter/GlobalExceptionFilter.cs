using He.Framework.Base;
using He.Framework.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace He.Framework.Filter
{
    /// <summary>
    /// 全局异常
    /// </summary>
    /// <remarks>
    /// 全局异常
    /// </remarks>
    /// <param name="environment"></param>
    /// <param name="logger">日志记录器</param>
    public class GlobalExceptionFilter(IWebHostEnvironment environment, ILogger<GlobalExceptionFilter> logger) : IExceptionFilter
    {
        private readonly IWebHostEnvironment environment = environment;
        private readonly ILogger<GlobalExceptionFilter> logger = logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled) // 如果异常没有处理
            {
                BaseResponse response = new BaseResponse(false, FrameworkEnum.StatusCode.ServerError);
                response.Exception = new BaseResponseException() { StackTrace = context.Exception.StackTrace ?? "", Source = context.Exception.Source ?? "", Message = context.Exception.Message ?? "" };
                if (environment.IsDevelopment()) { }
                logger.LogError(context.Exception, $"系统异常：{context.Exception.Message}");
                context.Result = new JsonResult(response);
                context.ExceptionHandled = true; // 异常已处理
            }
        }
    }
}
