using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebAppMVC
{
    public class ControllerActionInvoker:IActionInvoker
    {
        public IModelBinder ModelBinder { get; private set; }

        public ControllerActionInvoker()
        {
            this.ModelBinder = new DefaultModelBinder();
        }

        public void InvokeAction(ControllerContext controllerContext, string actionName)
        {
            //根据传入的 Action 名称作为方法名，从 Controller 类型中得到表示 Action 操作的 MethodInfo 对象
            MethodInfo method = controllerContext.Controller.GetType().GetMethods().First(m=>string.Compare(actionName, m.Name, true)==0);
            List<object> parameters = new List<object>();
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                parameters.Add(this.ModelBinder.BindModel(controllerContext, parameter.Name, parameter.ParameterType));
            }
            ActionResult actionResult = method.Invoke(controllerContext.Controller, parameters.ToArray()) as ActionResult;
            actionResult.ExecuteResult(controllerContext);
        }
    }
}
