using He.Framework.Base;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace He.UserService.Dal.Common
{
    /// <summary>
    /// 用户数据访问层
    /// </summary>
    /// <remarks>
    /// 用户数据访问层
    /// </remarks>
    public class UserServiceDal : HeServiceDal
    {
        /// <summary>
        /// 用户数据访问层
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="logger">日志记录器</param>
        public UserServiceDal(ISqlSugarClient db, ILogger<HeServiceDal> logger) : base(db, logger)
        {
        }
    }
}
