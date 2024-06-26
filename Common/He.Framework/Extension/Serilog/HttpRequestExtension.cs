﻿using Microsoft.AspNetCore.Http;
using System.Text;

namespace He.Framework.Extension.Serilog
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpRequestExtension
    {
        /// <summary>
        /// 获取请求IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRequestIp(this HttpContext context)
        {
            string? ipString = SplitCsv(GetHeaderString(context, "X-Forwarded-For")).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(ipString)) { ipString = SplitCsv(GetHeaderString(context, "X-Real-IP")).FirstOrDefault(); }
            if (string.IsNullOrWhiteSpace(ipString) && context.Connection?.RemoteIpAddress is not null) { ipString = context.Connection.RemoteIpAddress.ToString(); }
            if (string.IsNullOrWhiteSpace(ipString)) { ipString = GetHeaderString(context, "REMOTE_ADDR"); }
            return ipString;
        }

        /// <summary>
        /// 获取请求BODY
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<string> GetRequestBodyAsync(this HttpRequest request)
        {
            string bodyString = string.Empty;
            request.EnableBuffering();
            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true)) { bodyString = await reader.ReadToEndAsync(); }
            request.Body.Position = 0;
            return bodyString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        private static string GetHeaderString(HttpContext context, string headerName)
        {
            return GetHeaderValueAs<string>(context, headerName) ?? string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        private static T? GetHeaderValueAs<T>(HttpContext context, string headerName)
        {
            if (context.Request.Headers.TryGetValue(headerName, out var values))
            {
                string rawValues = values.ToString();
                if (!string.IsNullOrWhiteSpace(rawValues))
                {
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
                }
            }
            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvList"></param>
        /// <returns></returns>
        private static List<string> SplitCsv(string csvList)
        {
            if (string.IsNullOrWhiteSpace(csvList)) { return new List<string>(); }
            return csvList.TrimEnd(',').Split(',').AsEnumerable<string>().Select(s => s.Trim()).ToList();
        }
    }
}
