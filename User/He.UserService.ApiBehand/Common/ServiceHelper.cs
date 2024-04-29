using System.Reflection;

namespace He.UserService.ApiBehand.Common
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
