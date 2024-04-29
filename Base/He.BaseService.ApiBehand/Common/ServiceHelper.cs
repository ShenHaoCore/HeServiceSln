using System.Reflection;

namespace He.BaseService.ApiBehand.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceHelper
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        /// <returns></returns>
        public static string AssemblyName => $"{Assembly.GetExecutingAssembly().GetName().Name}";
    }
}
