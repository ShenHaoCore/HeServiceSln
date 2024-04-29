using AutoMapper;
using He.Framework.Base;
using He.Framework.Enum;
using He.UserService.Bll.Common;
using He.UserService.Dal;
using He.UserService.Model.DTO;
using He.UserService.Model.Entity;
using Microsoft.Extensions.Logging;

namespace He.UserService.Bll
{
    /// <summary>
    /// 身份证
    /// </summary>
    public class IdcardBll : UserServiceBll
    {
        #region 变量
        private readonly IdcardDal dal;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        /// <param name="dal">数据访问层</param>
        public IdcardBll(ILogger<HeServiceBll> logger, IMapper mapper, IdcardDal dal) : base(logger, mapper)
        {
            this.dal = dal;
        }

        /// <summary>
        /// 身份证创建方法
        /// </summary>
        /// <param name="paramObj">创建参数</param>
        /// <returns>创建结果</returns>
        public ResultModel<bool> Create(IdcardCreate paramObj)
        {
            t_IdentityCard idcard = mapper.Map<t_IdentityCard>(paramObj);
            if (!dal.Create(idcard)) { return new ResultModel<bool>(false, FrameworkEnum.StatusCode.Fail); }
            return new ResultModel<bool>(true, FrameworkEnum.StatusCode.Success);
        }
    }
}
