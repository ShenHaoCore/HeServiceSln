using He.Framework.Enum;
using static System.Net.Mime.MediaTypeNames;

namespace He.Framework.Base
{
    /// <summary>
    /// 返回结果基类接口
    /// </summary>
    interface IBaseResponse : IBase
    {
        /// <summary>
        /// 处理成功失败标志
        /// </summary>
        bool IsSuccess { get; set; }
    }
}
