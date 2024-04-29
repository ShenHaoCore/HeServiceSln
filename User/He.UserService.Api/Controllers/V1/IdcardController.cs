using Asp.Versioning;
using AutoMapper;
using He.Framework.Base;

namespace He.UserService.Api.Controllers.V1
{
    /// <summary>
    /// 身份证
    /// </summary>
    [ApiVersion(1.0)]
    public class IdcardController : HeBaseController
    {
        /// <summary>
        /// 身份证
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        public IdcardController(ILogger<HeBaseController> logger, IMapper mapper) : base(logger, mapper)
        {
        }
    }
}
