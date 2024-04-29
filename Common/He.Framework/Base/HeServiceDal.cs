using Microsoft.Extensions.Logging;
using SqlSugar;

namespace He.Framework.Base
{
    /// <summary>
    /// 数据访问层
    /// </summary>
    /// <param name="db">数据库</param>
    /// <param name="logger">日志记录器</param>
    /// <remarks>数据访问层</remarks>
    public class HeServiceDal(ISqlSugarClient db, ILogger<HeServiceDal> logger) : IHeServiceDal
    {
        /// <summary>
        /// 数据库
        /// </summary>
        public readonly ISqlSugarClient db = db;

        /// <summary>
        /// 日志
        /// </summary>
        public readonly ILogger<HeServiceDal> logger = logger;
    }
}
