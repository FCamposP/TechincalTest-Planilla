using Microsoft.AspNetCore.Mvc.Filters;
using Planilla.DTO;
using Planilla.DTO.Others;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Planilla.Attributes
{
    public class PermissionAuthorizationAttribute : AuthorizeAttribute,IFilterMetadata
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authorized = true;
            //ApiAccessClient AAClient = new ApiAccessClient();
            string controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            string action = actionContext.ActionDescriptor.ActionName;


            //access = AAClient.GetAccess(actionContext.ControllerContext.Request, controller, action);
            //authorized = (access.Data != null) ? access.Data.HasAccess : false;


            return false;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            ResponseWrapperDTO<string> response = new ResponseWrapperDTO<string>();
            response.AddRequestStatus(HttpStatusCode.Forbidden);
            response.Status.ResponseStatus = null;
            HttpResponseMessage responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(Json.Encode(response)),
                StatusCode = HttpStatusCode.Forbidden
            };
            responseMessage.Content.Headers.Remove("Content-Type");
            responseMessage.Content.Headers.Add("Content-Type", "application/json");
            actionContext.Response = responseMessage;
        }



    }
}
