using System.Net;
using System.Web.Helpers;
using System.Web.Http;
using Planilla.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Filters;
using Planilla.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Planilla.Abstractions;
using Planilla.Entities;
using Planilla.DTO.Others;
using Planilla.DTO;

namespace Planilla.Attributes
{
    public class CustomExceptionFilterAttribute : FilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter
    {
        private readonly RequestDelegate _next;

        //public CustomExceptionFilterAttribute(RequestDelegate next)
        //{
        //    _next = next;
        //}
        private readonly ErrorService LogErrorsService = new ErrorService();
        
        public async Task Invoke(HttpContext context, ILogErrorger LogErrorService)
        {
            LogErrorsService.Logger = LogErrorService;
            LogErrorService.RegisterException(new Exception());
            //await _next(context);

        }


        public void OnException(ExceptionContext actionExecutedContext)
        {
            Exception ex = actionExecutedContext.Exception;
            LogError exDTO = (LogError)ex;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            var statusMessage = "";

            ResponseWrapperDTO<string> response = new ResponseWrapperDTO<string>();

            response.Status.ResponseStatus = null;

            switch (ex.GetType().Name)
            {
                case "HttpResponseException":
                    var aux = (HttpResponseException)ex;
                    httpStatusCode = aux.Response.StatusCode;
                    response.AddRequestStatus(httpStatusCode, httpStatusCode.ToString());
                    exDTO.Message += " | " + aux.Response.StatusCode.ToString() + " | " + (aux.Response.ReasonPhrase ?? "");
                    break;
                case "EmptyInputException":
                    var aux1 = (EmptyInputException)ex;
                    httpStatusCode = HttpStatusCode.BadRequest;
                    response.AddRequestStatus(httpStatusCode, ex.Message);
                    response.AddResponseStatus(aux1.HResult,"Error por parámetro vacío", aux1.Message);
                    exDTO.Message += " | " + aux1.Message;
                    break;
                case "ApiException":
                    var aux2 = (ApiException)ex;

                    httpStatusCode = (aux2.StatusCode != 0) ? (HttpStatusCode)aux2.StatusCode : httpStatusCode;
                    response.AddRequestStatus(httpStatusCode, httpStatusCode.ToString());

                    if (aux2.ErrorCode != 0)
                    {
                        response.AddResponseStatus(aux2.ErrorCode,"Error en Api", aux2.Message);
                    }

                    exDTO.Message += " | " + aux2.ErrorCode.ToString() + " | " + aux2.Message + " | " + (aux2.ErrorContent ?? "");
                    break;
                default:
                    statusMessage = exDTO.ToString();
                    response.AddRequestStatus(httpStatusCode, statusMessage);
                    break;
            }
        }
    }
}
