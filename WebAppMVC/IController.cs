using System;
using System;
using System.Web;


namespace WebAppMVC
{
    public interface IController
    {
        void Execute(RequestContext requestContext);
    }
}
