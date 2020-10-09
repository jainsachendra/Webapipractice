﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
namespace UserService
{
    public class RequiredHttpsAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p> please use http</p>");
                UriBuilder uriBuilder = new UriBuilder(actionContext.Request.RequestUri);
                uriBuilder.Scheme = Uri.UriSchemeHttps;
                uriBuilder.Port = 44318;
                actionContext.Response.Headers.Location = uriBuilder.Uri;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
          
        }
    }
}