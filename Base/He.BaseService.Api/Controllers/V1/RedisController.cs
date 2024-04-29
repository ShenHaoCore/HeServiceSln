using Asp.Versioning;
using AutoMapper;
using He.Framework.Base;
using He.Framework.Enum;
using He.Framework.Extension.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace He.BaseService.Api.Controllers.V1
{
    /// <summary>
    /// Redis
    /// </summary>
    [ApiVersion(1.0)]
    public class RedisController : HeBaseController
    {
        private readonly IRedisManage redis;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        /// <param name="redis"></param>
        public RedisController(ILogger<HeBaseController> logger, IMapper mapper, IRedisManage redis) : base(logger, mapper)
        {
            this.redis = redis;
        }

        /// <summary>
        /// SET
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public BaseResponse Set()
        {
            bool result = this.redis.Set("TEST", "TEST", new TimeSpan(0, 30, 0));
            return new BaseResponse(true, FrameworkEnum.StatusCode.Success);
        }
    }
}
