using Autofac;
using AutoMapper;
using FluentValidation.Results;
using He.Business.Common;
using He.Business.Enum;
using He.Business.Payment;
using He.Common.Extension;
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
    /// 充值
    /// </summary>
    public class RechargeBll : UserServiceBll
    {
        #region 变量
        private readonly IComponentContext context;
        private readonly RechargeDal dal;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        /// <param name="context">组件上下文（IoC容器）</param>
        /// <param name="dal">数据访问层</param>
        public RechargeBll(ILogger<HeServiceBll> logger, IMapper mapper, IComponentContext context, RechargeDal dal) : base(logger, mapper)
        {
            this.dal = dal;
            this.context = context;
        }

        #region 方法
        /// <summary>
        /// APP充值方法
        /// </summary>
        /// <param name="paramObj">充值参数</param>
        /// <returns>充值结果</returns>
        public ResultModel<RechargeTradeModel> AppRecharge(RechargeTradeParam paramObj)
        {
            logger.LogDebug($"APP充值请求【{paramObj.ToJson()}】");
            RechargeTradeValidator validator = new RechargeTradeValidator();
            ValidationResult validResult = validator.Validate(paramObj);
            if (!validResult.IsValid) { return new ResultModel<RechargeTradeModel>(false, FrameworkEnum.StatusCode.ValidateFail); }
            IPayment iPay = context.ResolveKeyed<IPayment>((BusinessEnum.Payment)paramObj.Payment);
            var recharge = CreateTrade(paramObj);
            if (recharge is null) { return new ResultModel<RechargeTradeModel>(false, FrameworkEnum.StatusCode.Fail); }
            PaymentTrade trade = new PaymentTrade("支付充值", $"账户充值{recharge.Amount:f2}元", recharge.Amount, recharge.TradeNo);
            ResultModel<PaymentTradeOrder> payResult = iPay.AppTrade(trade);
            if (!payResult.IsSuccess) { return new ResultModel<RechargeTradeModel>(false, payResult.Code, payResult.Message); }
            if (payResult.Data is null) { return new ResultModel<RechargeTradeModel>(false, FrameworkEnum.StatusCode.NoData); }
            return new ResultModel<RechargeTradeModel>(true, FrameworkEnum.StatusCode.Success, new RechargeTradeModel(payResult.Data.Body));
        }

        /// <summary>
        /// 网页充值方法
        /// </summary>
        /// <param name="paramObj">充值参数</param>
        /// <returns>充值结果</returns>
        public ResultModel<RechargeTradeModel> PageRecharge(RechargeTradeParam paramObj)
        {
            logger.LogDebug($"网页充值请求【{paramObj.ToJson()}】");
            RechargeTradeValidator validator = new RechargeTradeValidator();
            ValidationResult validResult = validator.Validate(paramObj);
            if (!validResult.IsValid) { return new ResultModel<RechargeTradeModel>(false, FrameworkEnum.StatusCode.ValidateFail); }
            IPayment iPay = context.ResolveKeyed<IPayment>((BusinessEnum.Payment)paramObj.Payment);
            var recharge = CreateTrade(paramObj);
            if (recharge is null) { return new ResultModel<RechargeTradeModel>(false, FrameworkEnum.StatusCode.Fail); }
            PaymentTrade trade = new PaymentTrade("支付充值", $"账户充值{recharge.Amount:f2}元", recharge.Amount, recharge.TradeNo, paramObj.IsGet);
            ResultModel<PaymentTradeOrder> payResult = iPay.PageTrade(trade);
            if (!payResult.IsSuccess) { return new ResultModel<RechargeTradeModel>(false, payResult.Code, payResult.Message); }
            if (payResult.Data is null) { return new ResultModel<RechargeTradeModel>(false, FrameworkEnum.StatusCode.NoData); }
            return new ResultModel<RechargeTradeModel>(true, FrameworkEnum.StatusCode.Success, new RechargeTradeModel(payResult.Data.Body));
        }

        /// <summary>
        /// 创建交易
        /// </summary>
        /// <param name="paramObj">參數</param>
        /// <returns></returns>
        public t_RechargeTrade? CreateTrade(RechargeTradeParam paramObj)
        {
            t_RechargeTrade recharge = new t_RechargeTrade(OrderHelper.GetOrderNo(BusinessEnum.BusinessType.RE), paramObj.Amount, (int)BusinessEnum.Currency.CNY, paramObj.Payment);
            if (!dal.CreateTrade(recharge)) { return null; }
            return recharge;
        }
        #endregion
    }
}
