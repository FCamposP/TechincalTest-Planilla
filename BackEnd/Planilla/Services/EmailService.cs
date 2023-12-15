using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.Entities;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Planilla.Services
{
    public class EmailService : BaseService<EncabezadoPlanilla>
    {
        private ApiDBContext _dbContext;

        public EmailService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Envio de correo por carga de boleta de pago a empleado
        /// </summary>
        /// <param name="empleado"></param>
        /// <param name="fechaCorte"></param>
        /// <returns></returns>
        public async Task<bool> EnviarEmailEmpleados(Empleado empleado, DateTime? fechaCorte)
        {
            bool result = false;
            try
            {
                var correEnvio = await _dbContext.ConfiguracionGlobal.Where(x => x.Codigo == "CORREOPLANILLA").FirstOrDefaultAsync();
                var passwordEmail = await _dbContext.ConfiguracionGlobal.Where(x => x.Codigo == "PASSWORDEMAIL").FirstOrDefaultAsync();
                if (correEnvio != null)
                {

                    var fromAddress = new MailAddress(correEnvio.Valor, "Nombre Empresa");
                    var toAddress = new MailAddress(empleado.Email, empleado.PrimerNombre + " " + empleado.PrimerApellido);
                    string fromPassword = passwordEmail.Valor;
                    string subject = "Notificación de Boleta de Pago";
                    string body = empleado.PrimerNombre+" "+empleado.PrimerApellido+ ", Por este medio se le notifica que su información de boleta de pago correspondiente al corte " + fechaCorte.Value.ToString("yyyy-MM-dd") + " ha sido cargada al sistema.";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }


                }
            }
            catch (Exception ex)
            {
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Actualizar");
                exceptionHandler.SaveException(excepcion);
            }
            return result;
        }
    }
}
