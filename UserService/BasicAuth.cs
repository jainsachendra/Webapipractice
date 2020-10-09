using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace UserService
{
    public class BasicAuth:AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {

                string authenticationtoken = actionContext.Request.Headers.Authorization.Parameter;
                string token = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationtoken));
                string[] arrayuser = token.Split(':');
                string uname = arrayuser[0];
                string pass = arrayuser[1];
                if (EmpSecuirity.login(uname, pass)) {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(uname), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}