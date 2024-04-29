using System.Reflection;

namespace He.Framework.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameworkHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static string AssemblyName => $"{Assembly.GetExecutingAssembly().GetName().Name}";
    }
}
