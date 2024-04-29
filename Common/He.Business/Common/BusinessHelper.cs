using System.Reflection;

namespace He.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static string AssemblyName => $"{Assembly.GetExecutingAssembly().GetName().Name}";
    }
}
