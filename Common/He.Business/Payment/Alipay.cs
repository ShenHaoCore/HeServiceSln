using He.Framework.Base;
using He.Framework.Enum;

namespace He.Business.Payment
{
    /// <summary>
    /// 支付宝
    /// </summary>
    public class Alipay : IPayment
    {
        /// <summary>
        /// APP交易
        /// </summary>
        /// <param name="paramObj"></param>
        /// <returns></returns>
        public ResultModel<PaymentTradeOrder> AppTrade(PaymentTrade paramObj)
        {
            return new ResultModel<PaymentTradeOrder>(true, FrameworkEnum.StatusCode.Success, new PaymentTradeOrder(""));
        }

        /// <summary>
        /// 网页交易
        /// </summary>
        /// <param name="paramObj"></param>
        /// <returns></returns>
        public ResultModel<PaymentTradeOrder> PageTrade(PaymentTrade paramObj)
        {
            return new ResultModel<PaymentTradeOrder>(true, FrameworkEnum.StatusCode.Success, new PaymentTradeOrder(""));
        }
    }
}
