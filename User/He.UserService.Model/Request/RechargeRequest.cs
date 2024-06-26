﻿namespace He.UserService.Model.Request
{
    /// <summary>
    /// 充值请求
    /// </summary>
    public class RechargeRequest
    {
        /// <summary>
        /// (必填)金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// (必填)支付平台
        /// <para>1：支付宝[Alipay]</para>
        /// <para>2：微信[WeChat]</para>
        /// <para>3：银联[UnionPay]</para>
        /// </summary>
        public int Payment { get; set; }
    }

    /// <summary>
    /// 充值请求
    /// </summary>
    public class AppRechargeRequest : RechargeRequest
    {
    }

    /// <summary>
    /// 充值请求
    /// </summary>
    public class PageRechargeRequest : RechargeRequest
    {
        /// <summary>
        /// (可选)是否生成GET请求URL
        /// </summary>
        public bool? IsGet { get; set; }
    }
}
