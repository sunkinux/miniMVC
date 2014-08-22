using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAppMVC
{
    /// <summary>
    /// 对当前 Controller 和请求上下文的封装
    /// </summary>
    public class ControllerContext
    {
        public ControllerBase Controller { get; set; }
        public RequestContext RequestContext { get; set; }
    }
}
