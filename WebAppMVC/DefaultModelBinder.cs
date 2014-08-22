using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;

namespace WebAppMVC
{
    public class DefaultModelBinder:IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, string modelName, Type modelType)
        { 
            //如果参数类型为字符或者值类型，就根据参数名称和 Key 进行匹配
            if(modelType.IsValueType || typeof(string)==modelType)
            {
                object instance;
                if(GetValueTypeInstance(controllerContext, modelName, modelType, out instance))
                    return instance;
                return Activator.CreateInstance(modelType);
            }
            
            //对于复杂类型，则通过反射根据类型创建新的对象，并根据属性名称与 Key 的匹配关系对相应的属性进行赋值
            object modelInstance = Activator.CreateInstance(modelType);
            foreach (PropertyInfo property in modelType.GetProperties())
            {
                if (!property.CanWrite || (!property.PropertyType.IsValueType && property.PropertyType != typeof(string)))
                    continue;
                object propertyValue;
                if (GetValueTypeInstance(controllerContext, property.Name, property.PropertyType, out propertyValue))
                    property.SetValue(modelInstance, propertyValue, null);
            }
            return modelInstance;
        }

        private bool GetValueTypeInstance(ControllerContext controllerContext, string modelName, Type modelType, out object value)
        {
            var form = HttpContext.Current.Request.Form;
            string key;
            if (null != form)
            {
                key = form.AllKeys.FirstOrDefault(k => string.Compare(k, modelName, true) == 0);
                if (key != null)
                {
                    value = Convert.ChangeType(form[key], modelType);
                    return true;
                }
            }

            key = controllerContext.RequestContext.RouteData.Values
                .Where(item => string.Compare(item.Key, modelName, true) == 0)
                .Select(item => item.Key).FirstOrDefault();
            if (null != key)
            {
                value = Convert.ChangeType(controllerContext.RequestContext.RouteData.Values[key], modelType);
                return true;
            }

            key = controllerContext.RequestContext.RouteData.DataTokens
                .Where(item => string.Compare(item.Key, modelName, true) == 0)
                .Select(item => item.Key).FirstOrDefault();
            if (null != key)
            {
                value = Convert.ChangeType(controllerContext.RequestContext.RouteData.DataTokens[key], modelType);
                return true;
            }
            value = null;
            return false;
        }
    }
}
