using Asp.Versioning;
using AutoMapper;
using He.Framework.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace He.BaseService.ApiBehand.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion(1.0)]
    public class ValuesController : HeBaseController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        public ValuesController(ILogger<HeBaseController> logger, IMapper mapper) : base(logger, mapper)
        {
        }

        private readonly List<string> values = ["value1", "value2"];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> Get() => values;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<string> Get(int id)
        {
            if (id < 0 || id >= values.Count) { return NotFound(); }
            return values[id];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post([FromBody] string value)
        {
            values.Add(value);
            return CreatedAtAction(nameof(Get), new { id = values.Count - 1 }, value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [AllowAnonymous]
        public IActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0 || id >= values.Count) { return NotFound(); }
            values[id] = value;
            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public IActionResult Delete(int id)
        {
            if (id < 0 || id >= values.Count) { return NotFound(); }
            values.RemoveAt(id);
            return NoContent();
        }
    }
}
