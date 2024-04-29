using AutoMapper;
using He.BaseService.Bll.Common;
using Microsoft.Extensions.Logging;

namespace He.BaseService.Bll
{
    /// <summary>
    /// 存储业务逻辑层
    /// </summary>
    public class StorageBll : BaseServiceBll
    {
        /// <summary>
        /// 存储业务逻辑层
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        public StorageBll(ILogger<BaseServiceBll> logger, IMapper mapper) : base(logger, mapper)
        {
        }
    }
}
