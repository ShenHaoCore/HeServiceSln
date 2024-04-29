using Microsoft.AspNetCore.Http;

namespace He.BaseService.Model.Request
{
    /// <summary>
    /// 上传文件请求
    /// </summary>
    public class UploadFileRequest
    {
        /// <summary>
        /// 文件列表
        /// </summary>
        public required List<IFormFile> FileList { get; set; }

        /// <summary>
        /// 是否临时文件
        /// </summary>
        public bool IsTemp { get; set; } = true;
    }
}
