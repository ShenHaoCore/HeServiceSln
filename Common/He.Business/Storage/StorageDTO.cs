using Microsoft.AspNetCore.Http;

namespace He.Business.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadModel
    {
        /// <summary>
        /// 文件
        /// </summary>
        public required IFormFile File { get; set; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string Path { get; set; } = string.Empty;
    }
}
