﻿using Autofac;
using He.BaseService.Bll.Common;
using He.BaseService.Dal.Common;
using He.Business.Enum;
using He.Business.Payment;
using Microsoft.AspNetCore.Mvc;

namespace He.BaseService.Api.Common
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
            builder.RegisterAssemblyTypes(typeof(BaseServiceBll).Assembly).Where(t => typeof(BaseServiceBll).IsAssignableFrom(t) && t != typeof(BaseServiceBll)).PropertiesAutowired();
            builder.RegisterAssemblyTypes(typeof(BaseServiceDal).Assembly).Where(t => typeof(BaseServiceDal).IsAssignableFrom(t) && t != typeof(BaseServiceDal)).PropertiesAutowired();
        }
    }
}
