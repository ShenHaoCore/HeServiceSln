﻿using He.Framework.Attribute;
using He.Framework.Base;

namespace He.Framework.Common
{
    /// <summary>
    /// 服务配置
    /// </summary>
    [ConfigTag("Service")]
    public class ServiceConfig : ConfigModel
    {
        /// <summary>
        /// KEY
        /// </summary>
        public const string KEY = "Service";

        /// <summary>
        /// 前缀名称
        /// </summary>
        public string PrefixName { get; set; } = string.Empty;
    }
}
