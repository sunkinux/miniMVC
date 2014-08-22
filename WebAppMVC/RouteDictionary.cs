using System;
using System.Collections.Generic;
using System.Web;

namespace WebAppMVC
{
    public class RouteDictionary: Dictionary<string, RouteBase>
    {
        public RouteData GetRouteData(HttpContextBase httpContext)
        {
            foreach (var route in this.Values)
            {
                RouteData routeData = route.GetRouteData(httpContext);
                if (routeData != null)
                    return routeData;
            }
            return null;
        }
    }
}
