using System.ComponentModel;

namespace He.Business.Enum
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessEnum
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        public enum BusinessType
        {
            /// <summary>
            /// 充值
            /// </summary>
            [Description("充值")]
            RE = 1,
        }

        /// <summary>
        /// 支付平台
        /// </summary>
        public enum Payment
        {
            /// <summary>
            /// 支付宝
            /// </summary>
            [Description("支付宝")]
            Alipay = 1,

            /// <summary>
            /// 微信
            /// </summary>
            [Description("微信")]
            WeChat = 2,

            /// <summary>
            /// 银联
            /// </summary>
            [Description("银联")]
            UnionPay = 3,
        }

        /// <summary>
        /// 币种
        /// </summary>
        public enum Currency
        {
            /// <summary>
            /// CNY
            /// </summary>
            [Description("CNY")]
            CNY = 1,

            /// <summary>
            /// USD
            /// </summary>
            [Description("USD")]
            USD = 2,
        }
    }
}
