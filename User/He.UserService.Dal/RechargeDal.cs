using He.Framework.Base;
using He.UserService.Dal.Common;
using He.UserService.Model.Entity;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace He.UserService.Dal
{
    /// <summary>
    /// 充值
    /// </summary>
    public class RechargeDal : UserServiceDal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="logger">日志记录器</param>
        public RechargeDal(ISqlSugarClient db, ILogger<HeServiceDal> logger) : base(db, logger)
        {
        }


        /// <summary>
        /// 创建充值交易
        /// </summary>
        /// <param name="recharge"></param>
        /// <returns></returns>
        public bool CreateTrade(t_RechargeTrade recharge) => db.Insertable<t_RechargeTrade>(recharge).ExecuteCommand() > 0;
    }
}
