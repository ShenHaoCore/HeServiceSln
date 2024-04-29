using He.Common.Extension;
using He.Framework.Extension.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace He.Framework.Extension.Serilog
{
    /// <summary>
    /// Serilog安装服务
    /// </summary>
    public static class SerilogExtension
    {
        private static readonly string _logTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] 日志信息：{Message:lj}{NewLine}";
        private static readonly string _errorTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] 日志信息：{Message:lj}{NewLine}{Exception}{NewLine}";
        private static readonly string _customTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms"; // 自定义消息模板（Customize The Message Template）

        /// <summary>
        /// Serilog注册
        /// </summary>
        /// <param name="host"></param>
        public static IHostBuilder AddSerilog(this IHostBuilder host)
        {
            ArgumentNullException.ThrowIfNull(host);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteToFileAsync()
                .CreateBootstrapLogger();

            host.UseSerilog();
            return host;
        }

        /// <summary>
        /// 异步写入文件
        /// </summary>
        /// <param name="logConfig"></param>
        /// <returns></returns>
        public static LoggerConfiguration WriteToFileAsync(this LoggerConfiguration logConfig)
        {
            logConfig = logConfig.WriteTo.Async(P => P.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/LOG_.log"), rollingInterval: RollingInterval.Day, outputTemplate: _logTemplate));
            logConfig = logConfig.WriteTo.Logger(G => G.Filter.ByIncludingOnly(P => P.Level == LogEventLevel.Error).WriteTo.Async(P => P.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/ERR_.log"), rollingInterval: RollingInterval.Day, outputTemplate: _errorTemplate)));
            return logConfig;
        }

        /// <summary>
        /// 使用Serilog
        /// </summary>
        /// <param name="app"></param>
        public static void UseSerilog(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(options => { options.MessageTemplate = _customTemplate; options.GetLevel = GetLogLevel; options.EnrichDiagnosticContext = EnrichFromRequest; });
        }

        /// <summary>
        /// 获取日志等级
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="elapsed"></param>
        /// <param name="ex"></param>
        public static LogEventLevel GetLogLevel(HttpContext httpContext, double elapsed, Exception? ex)
        {
            if (ex is not null) { return LogEventLevel.Error; }
            return LogEventLevel.Debug;
        }

        /// <summary>
        /// 从Request中增加附属属性
        /// </summary>
        /// <param name="diagnosticContext"></param>
        /// <param name="httpContext"></param>
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();

            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("Protocol", httpContext.Request.Protocol);
            diagnosticContext.Set("RequestIp", httpContext.GetRequestIp());
            diagnosticContext.Set("QueryString", httpContext.Request.QueryString.HasValue ? httpContext.Request.QueryString.Value : string.Empty);
            diagnosticContext.Set("Body", httpContext.Request.ContentLength > 0 ? httpContext.Request.GetRequestBodyAsync() : string.Empty);
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);
            diagnosticContext.Set("EndpointName", endpoint == null ? string.Empty : endpoint.DisplayName);
        }
    }
}
