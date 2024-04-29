using AutoMapper;
using He.Framework.Base;
using Microsoft.Extensions.Logging;

namespace He.BaseService.Bll.Common
{
    /// <summary>
    /// 基础业务逻辑层
    /// </summary>
    /// <remarks>
    /// 基础业务逻辑层
    /// </remarks>
    public class BaseServiceBll : HeServiceBll
    {
        /// <summary>
        /// 基础业务逻辑层
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        public BaseServiceBll(ILogger<BaseServiceBll> logger, IMapper mapper) : base(logger, mapper)
        {
        }
    }
}
