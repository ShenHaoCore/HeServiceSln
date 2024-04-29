using Asp.Versioning;
using AutoMapper;
using He.Common.Extension;
using He.Framework.Base;
using He.Framework.Enum;
using He.UserService.Bll;
using He.UserService.Model.DTO;
using He.UserService.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace He.UserService.Api.Controllers.V1
{
    /// <summary>
    /// 充值
    /// </summary>
    [ApiVersion(1.0)]
    public class RechargeController : HeBaseController
    {
        #region 变量
        private readonly RechargeBll bll;
        #endregion

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        /// <param name="bll">业务逻辑层</param>
        public RechargeController(ILogger<HeBaseController> logger, IMapper mapper, RechargeBll bll) : base(logger, mapper)
        {
            this.bll = bll;
        }

        #region 接口
        /// <summary>
        /// APP充值
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponseObject<RechargeTradeModel> App([FromBody] AppRechargeRequest request)
        {
            if (request is null) { return new BaseResponseObject<RechargeTradeModel>(false, FrameworkEnum.StatusCode.NullRequest); }
            logger.LogDebug($"APP充值请求【{request.ToJson()}】");
            RechargeTradeParam paramObj = new RechargeTradeParam(request.Amount, request.Payment);
            ResultModel<RechargeTradeModel> result = bll.AppRecharge(paramObj);
            if (!result.IsSuccess) { return new BaseResponseObject<RechargeTradeModel>(false, result.Code, result.Message); }
            if (result.Data is null) { return new BaseResponseObject<RechargeTradeModel>(false, FrameworkEnum.StatusCode.NoData); }
            return new BaseResponseObject<RechargeTradeModel>(true, result.Code, result.Message, result.Data);
        }

        /// <summary>
        /// 网页充值
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponseObject<RechargeTradeModel> Page([FromBody] PageRechargeRequest request)
        {
            if (request is null) { return new BaseResponseObject<RechargeTradeModel>(false, FrameworkEnum.StatusCode.NullRequest); }
            logger.LogDebug($"网页充值请求【{request.ToJson()}】");
            RechargeTradeParam paramObj = new RechargeTradeParam(request.Amount, request.Payment, request.IsGet ?? false);
            ResultModel<RechargeTradeModel> result = bll.PageRecharge(paramObj);
            if (!result.IsSuccess) { return new BaseResponseObject<RechargeTradeModel>(false, result.Code, result.Message); }
            if (result.Data is null) { return new BaseResponseObject<RechargeTradeModel>(false, FrameworkEnum.StatusCode.NoData); }
            return new BaseResponseObject<RechargeTradeModel>(true, result.Code, result.Message, result.Data);
        }
        #endregion
    }
}
