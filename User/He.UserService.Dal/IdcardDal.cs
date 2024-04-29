using He.Framework.Base;
using He.UserService.Dal.Common;
using He.UserService.Model.Entity;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace He.UserService.Dal
{
    /// <summary>
    /// 身份证
    /// </summary>
    public class IdcardDal : UserServiceDal
    {
        /// <summary>
        /// 身份证
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger">日志记录器</param>
        public IdcardDal(ISqlSugarClient db, ILogger<HeServiceDal> logger) : base(db, logger)
        {
        }

        /// <summary>
        /// 身份证创建方法
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        public bool Create(t_IdentityCard idcard)
        {
            idcard.ID = db.Insertable(idcard).ExecuteReturnIdentity();
            return true;
        }
    }
}
