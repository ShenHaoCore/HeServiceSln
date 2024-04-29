using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace He.Framework.Base
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    /// <remarks>所有控制器继承于基础控制器</remarks>
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class HeBaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly ILogger<HeBaseController> logger;

        /// <summary>
        /// 
        /// </summary>
        public readonly IMapper mapper;

        /// <summary>
        /// 基础控制器
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        public HeBaseController(ILogger<HeBaseController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
