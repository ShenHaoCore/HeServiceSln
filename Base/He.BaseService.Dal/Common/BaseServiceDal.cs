using He.Framework.Base;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace He.BaseService.Dal.Common
{
    /// <summary>
    /// 基础数据访问层
    /// </summary>
    /// <remarks>
    /// 基础数据访问层
    /// </remarks>
    public class BaseServiceDal : HeServiceDal
    {
        /// <summary>
        /// 基础数据访问层
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="logger">日志记录器</param>
        public BaseServiceDal(ISqlSugarClient db, ILogger<HeServiceDal> logger) : base(db, logger)
        {
        }
    }
}
