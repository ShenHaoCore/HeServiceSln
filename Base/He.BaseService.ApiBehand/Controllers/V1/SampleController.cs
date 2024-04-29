using Asp.Versioning;
using AutoMapper;
using He.Framework.Base;
using Microsoft.AspNetCore.Mvc;

namespace He.BaseService.ApiBehand.Controllers.V1
{
    /// <summary>
    /// 测试
    /// </summary>
    [ApiVersion(1.0)]
    public class SampleController : HeBaseController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        public SampleController(ILogger<HeBaseController> logger, IMapper mapper) : base(logger, mapper)
        {
        }

        /// <summary>
        /// GET
        /// </summary>
        [HttpGet]
        public ActionResult Get()
        {
            return Content("SUCCESS");
        }

        /// <summary>
        /// POST
        /// </summary>
        [HttpPost]
        public ActionResult Post()
        {
            return Content("SUCCESS");
        }
    }
}
