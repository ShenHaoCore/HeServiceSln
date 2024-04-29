using He.Framework.Base;

namespace He.Business.Payment
{
    /// <summary>
    /// 支付平台
    /// </summary>
    public interface IPayment
    {
        /// <summary>
        /// APP交易
        /// </summary>
        /// <param name="paramObj"></param>
        /// <returns></returns>
        ResultModel<PaymentTradeOrder> AppTrade(PaymentTrade paramObj);

        /// <summary>
        /// 网页交易
        /// </summary>
        /// <param name="paramObj"></param>
        /// <returns></returns>
        ResultModel<PaymentTradeOrder> PageTrade(PaymentTrade paramObj);
    }
}
