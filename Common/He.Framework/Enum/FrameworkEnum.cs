using System.ComponentModel;

namespace He.Framework.Enum
{
    /// <summary>
    /// 框架枚举
    /// </summary>
    public class FrameworkEnum
    {
        /// <summary>
        /// 状态代码
        /// </summary>
        public enum StatusCode
        {
            /// <summary>
            /// 未授权
            /// </summary>
            [Description("未授权")]
            UnAuthorized = 401,

            /// <summary>
            /// 权限不足
            /// </summary>
            [Description("权限不足")]
            Forbidden = 403,

            /// <summary>
            /// 频繁请求请稍后在试
            /// </summary>
            [Description("频繁请求")]
            TooManyRequests = 429,

            /// <summary>
            /// 服务器遇到错误无法完成请求
            /// </summary>
            [Description("服务器内部错误")]
            ServerError = 500,

            /// <summary>
            /// 服务器已成功处理了请求
            /// </summary>
            [Description("成功")]
            Success = 10000,

            /// <summary>
            /// 失败
            /// </summary>
            [Description("失败")]
            Fail = 10001,

            /// <summary>
            /// 服务器接收到的请求参数为空
            /// </summary>
            [Description("请求为空")]
            NullRequest = 10002,

            /// <summary>
            /// 数据验证失败
            /// </summary>
            [Description("验证失败")]
            ValidateFail = 10003,

            /// <summary>
            /// 未开通此服务
            /// </summary>
            [Description("无服务")]
            NoService = 10006,

            /// <summary>
            /// 无数据
            /// </summary>
            [Description("无数据")]
            NoData = 10007,
        }
    }
}
