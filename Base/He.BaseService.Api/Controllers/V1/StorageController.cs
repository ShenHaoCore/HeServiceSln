using AutoMapper;
using He.BaseService.Bll;
using He.BaseService.Model.DTO;
using He.BaseService.Model.Request;
using He.Business.Storage;
using He.Framework.Base;
using He.Framework.Enum;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace He.BaseService.Api.Controllers.V1
{
    /// <summary>
    /// 存储
    /// </summary>
    public class StorageController : HeBaseController
    {
        private readonly StorageBll bll;

        /// <summary>
        /// 存储
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mapper">对象映射器</param>
        /// <param name="bll">业务逻辑层</param>
        public StorageController(ILogger<HeBaseController> logger, IMapper mapper, StorageBll bll) : base(logger, mapper)
        {
            this.bll = bll;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [HttpPost]
        [Description("上传文件")]
        public BaseResponseList<UploadModel> UploadFile([FromForm] UploadFile request)
        {
            List<UploadModel> uploads = StorageHelper.SaveFileTemp(request.Files);
            return new BaseResponseList<UploadModel>(true, FrameworkEnum.StatusCode.Success, uploads);
        }
    }
}
