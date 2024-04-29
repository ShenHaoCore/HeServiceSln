using AutoMapper;
using He.Framework.Base;
using Microsoft.Extensions.Logging;

namespace He.UserService.Bll.Common
{
    /// <summary>
    /// 用户业务逻辑层
    /// </summary>
    /// <remarks>
    /// 用户业务逻辑层
    /// </remarks>
    public class UserServiceBll : HeServiceBll
    {
        /// <summary>
        /// 用户业务逻辑层
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        public UserServiceBll(ILogger<HeServiceBll> logger, IMapper mapper) : base(logger, mapper)
        {
        }
    }
}
