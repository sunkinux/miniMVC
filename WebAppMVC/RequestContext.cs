using System;
using System.Web;

namespace WebAppMVC
{
    /// <summary>
    /// 对当前 HttpContext 和 RouteData 封装
    /// </summary>
    public class RequestContext
    {
        public virtual HttpContextBase HttpContext { get; set; }
        public virtual RouteData RouteData { get; set; }
    }
}
