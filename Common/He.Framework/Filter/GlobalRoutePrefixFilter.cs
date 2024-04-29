using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace He.Framework.Filter
{
    /// <summary>
    /// 全局路由前缀
    /// </summary>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    /// <param name="templateProvider"></param>
    public class GlobalRoutePrefixFilter(IRouteTemplateProvider templateProvider) : IApplicationModelConvention
    {
        private readonly AttributeRouteModel prefix = new AttributeRouteModel(templateProvider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        public void Apply(ApplicationModel application)
        {
            foreach (ControllerModel controller in application.Controllers)
            {
                foreach (SelectorModel selector in controller.Selectors)
                {
                    if (selector.AttributeRouteModel is null) { selector.AttributeRouteModel = prefix; }
                    if (selector.AttributeRouteModel is not null) { selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(prefix, selector.AttributeRouteModel); }
                }
            }
        }
    }
}
