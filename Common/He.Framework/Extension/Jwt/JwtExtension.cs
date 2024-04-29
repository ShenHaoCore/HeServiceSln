using He.Common.Extension;
using He.Framework.Base;
using He.Framework.Common;
using He.Framework.Enum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace He.Framework.Extension.Jwt
{
    /// <summary>
    /// 
    /// </summary>
    public static class JwtExtension
    {
        /// <summary>
        /// JWT服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddJwt(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            var setting = AppSettings.GetObject<JwtConfig>();
            ArgumentNullException.ThrowIfNull(setting);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(setting.SecretKey);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuerSigningKey = true,
                    ValidAudience = setting.Audience,
                    ValidIssuer = setting.Issuer,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = Challenge,
                    OnForbidden = Forbidden
                };
            });
        }

        /// <summary>
        /// 如果授权失败并导致禁止响应，则调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task Forbidden(ForbiddenContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.WriteAsync(new BaseResponse(false, FrameworkEnum.StatusCode.Forbidden).ToJson());
            return Task.CompletedTask;
        }

        /// <summary>
        /// 在质询发送回调用方之前调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task Challenge(JwtBearerChallengeContext context)
        {
            context.HandleResponse();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.WriteAsync(new BaseResponse(false, FrameworkEnum.StatusCode.UnAuthorized).ToJson());
            return Task.CompletedTask;
        }
    }
}
