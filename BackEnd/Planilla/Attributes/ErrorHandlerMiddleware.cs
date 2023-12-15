using Microsoft.AspNetCore.Http;
using Planilla.Abstractions;
using Planilla.DTO.Others;
using Planilla.Entities;
using Planilla.Exceptions;
using Planilla.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;

namespace Planilla.Attributes
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ErrorService LogErrorsService = new ErrorService();

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Middelware para excepciones
        /// </summary>
        /// <param name="context"></param>
        /// <param name="LogErrorgerDB"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ILogErrorger LogErrorgerDB)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogErrorsService.Logger = LogErrorgerDB;
                var response = context.Response;
                response.ContentType = "application/json";
                LogError exDTO = (LogError)ex;
                HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
                var statusMessage = "";
                ResponseWrapperDTO<string> responseW = new ResponseWrapperDTO<string>();
                responseW.AddResponseStatus(0, exDTO.Message ?? "", "Ocurrió un error");

                switch (ex.GetType().Name)
                {
                    case "HttpResponseException":
                        var aux = (HttpResponseException)ex;
                        httpStatusCode = aux.Response.StatusCode;
                        responseW.AddRequestStatus(httpStatusCode, httpStatusCode.ToString());
                        exDTO.Message += " | " + aux.Response.StatusCode.ToString() + " | " + (aux.Response.ReasonPhrase ?? "");
                        break;
                    case "EmptyInputException":
                        var aux1 = (EmptyInputException)ex;
                        httpStatusCode = HttpStatusCode.BadRequest;
                        responseW.AddRequestStatus(httpStatusCode, ex.Message);
                        responseW.AddResponseStatus(aux1.HResult, aux1.Message, "Error por parámetro vacío");
                        exDTO.Message += " | " + aux1.Message;
                        break;
                    case "ApiException":
                        var aux2 = (ApiException)ex;
                        httpStatusCode = (aux2.StatusCode != 0) ? (HttpStatusCode)aux2.StatusCode : httpStatusCode;
                        responseW.AddRequestStatus(httpStatusCode, httpStatusCode.ToString());

                        if (aux2.ErrorCode != 0)
                        {
                            responseW.AddResponseStatus(0, aux2.Message, "Error en Api");
                        }

                        exDTO.Message += " | " + aux2.ErrorCode.ToString() + " | " + aux2.Message + " | " + (aux2.ErrorContent ?? "");
                        break;
                    default:
                        httpStatusCode = HttpStatusCode.InternalServerError;
                        statusMessage = exDTO.ToString();
                        responseW.AddRequestStatus(httpStatusCode);
                        responseW.AddResponseStatus(0, statusMessage ?? "", "Ocurrió un error");
                        break;
                }
                responseW.Data = exDTO.Message;

                var result = JsonSerializer.Serialize(responseW);
                await response.WriteAsync(result);

                LogErrorsService.RegisterException(exDTO);
            }
        }
    }
}