using System.Reflection;

namespace He.BaseService.Api.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string AssemblyName => $"{Assembly.GetExecutingAssembly().GetName().Name}";
    }
}
