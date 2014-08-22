﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVC
{
    /// <summary>
    /// 以 Controller 和 Action名称为核心的路由数据
    /// </summary>
    public class RouteData
    {
        /// <summary>
        /// 直接从请求地址解析出来的变量列表
        /// </summary>
        public IDictionary<string, object> Values { get; private set; }

        public IDictionary<string, object> DataTokens { get; private set; }

        public IRouteHandler RouteHandler { get; set; }
        public RouteBase Route { get; set; } //生成路由数据对应的路由对象

        public RouteData()
        {
            this.Values = new Dictionary<string, object>();
            this.DataTokens = new Dictionary<string, object>();
            this.DataTokens.Add("namespaces", new List<string>());
        }

        public string Controller
        {
            get
            {
                object controllerName = string.Empty;
                this.Values.TryGetValue("controller", out controllerName);
                return controllerName.ToString();
            }
        }
        public string ActionName
        {
            get
            {
                object actionName = string.Empty;
                this.Values.TryGetValue("action", out actionName);
                return actionName.ToString();
            }
        }
    }
}