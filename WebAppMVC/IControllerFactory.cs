﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAppMVC
{
    public interface IControllerFactory
    {
        IController CreateController(RequestContext requestContext, string controllerName);
    }
}
