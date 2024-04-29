using Microsoft.AspNetCore.Http;

namespace He.Business.Storage
{
    /// <summary>
    /// 存储帮助类
    /// </summary>
    public class StorageHelper
    {
        /// <summary>
        /// 保存到临时目录
        /// </summary>
        /// <param name="files">文件</param>
        public static List<UploadModel> SaveFileTemp(List<IFormFile> files)
        {
            List<UploadModel> uploads = files.Select(file => new UploadModel() { File = file, Path = string.Empty }).ToList();
            Parallel.ForEach(uploads, SaveFileTemp);
            return uploads;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="isTemp"></param>
        /// <returns></returns>
        public static List<UploadModel> SaveFile(List<IFormFile> files, bool isTemp)
        {
            List<UploadModel> uploads = files.Select(file => new UploadModel() { File = file, Path = string.Empty }).ToList();
            Parallel.ForEach(uploads, SaveFileTemp);
            return uploads;
        }

        /// <summary>
        /// 保存到临时目录
        /// </summary>
        /// <param name="upload">上传</param>
        public static void SaveFileTemp(UploadModel upload)
        {
            upload.Path = SaveFileTemp(upload.File);
        }

        /// <summary>
        /// 保存到临时目录
        /// </summary>
        /// <param name="file">文件</param>
        public static string SaveFileTemp(IFormFile file)
        {
            string path = GetTempPath(file);
            SaveFile(file, path);
            return path;
        }

        /// <summary>
        /// 获取临时临时路径
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetTempPath(IFormFile file)
        {
            string dire = TempDirectory;
            if (!Directory.Exists(dire)) { Directory.CreateDirectory(dire); }
            return $"{dire}/{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="path">文件路径</param>
        public static void SaveFile(IFormFile file, string path)
        {
            using FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }

        /// <summary>
        /// 临时文件目录
        /// </summary>
        public static string TempDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Temp/{DateTime.Now:yyyy-MM-dd}");
    }
}
