using AutoMapper;
using Microsoft.Extensions.Logging;

namespace He.Framework.Base
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    /// <remarks>
    /// 业务逻辑层
    /// </remarks>
    /// <param name="logger">日志记录器</param>
    /// <param name="mapper">对象映射器</param>
    public class HeServiceBll(ILogger<HeServiceBll> logger, IMapper mapper) : IHeServiceBll
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        public readonly ILogger<HeServiceBll> logger = logger;

        /// <summary>
        /// 映射
        /// </summary>
        public readonly IMapper mapper = mapper;
    }
}