using Microsoft.AspNetCore.Http;

namespace He.BaseService.Model.DTO
{
    /// <summary>
    /// 上传
    /// </summary>
    public class UploadFile
    {
        /// <summary>
        /// 文件
        /// </summary>
        public required List<IFormFile> Files { get; set; }

        /// <summary>
        /// 是否临时文件
        /// </summary>
        public bool IsTemp { get; set; } = true;
    }
}
