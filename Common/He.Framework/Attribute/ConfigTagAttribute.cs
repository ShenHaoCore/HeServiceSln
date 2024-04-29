namespace He.Framework.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigTagAttribute : System.Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        public ConfigTagAttribute(string key)
        {
            Key = key;
        }
    }
}
