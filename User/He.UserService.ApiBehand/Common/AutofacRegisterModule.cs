using Autofac;
using He.Business.Enum;
using He.Business.Payment;
using He.UserService.Bll.Common;
using He.UserService.Dal.Common;
using Microsoft.AspNetCore.Mvc;

namespace He.UserService.ApiBehand.Common
{
    /// <summary>
    /// 自动注册模块
    /// </summary>
    public class AutofacRegisterModule : Autofac.Module
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t != typeof(ControllerBase)).PropertiesAutowired();
            builder.RegisterAssemblyTypes(typeof(UserServiceBll).Assembly).Where(t => typeof(UserServiceBll).IsAssignableFrom(t) && t != typeof(UserServiceBll)).PropertiesAutowired();
            builder.RegisterAssemblyTypes(typeof(UserServiceDal).Assembly).Where(t => typeof(UserServiceDal).IsAssignableFrom(t) && t != typeof(UserServiceDal)).PropertiesAutowired();
            builder.RegisterType<Alipay>().Keyed<IPayment>(BusinessEnum.Payment.Alipay);
            builder.RegisterType<Business.Payment.WeChat>().Keyed<IPayment>(BusinessEnum.Payment.WeChat);
            builder.RegisterType<UnionPay>().Keyed<IPayment>(BusinessEnum.Payment.UnionPay);
        }
    }
}
