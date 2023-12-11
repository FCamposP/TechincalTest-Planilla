using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planilla.Attributes
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        //private RolUserService _rolUserService;
        public List<string> valoresPermitidos = new List<string>();

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
            DefinirListaPermitida();
        }

        private void DefinirListaPermitida()
        {
            valoresPermitidos.Add("/swagger");
            valoresPermitidos.Add("/swagger/index.html");
            valoresPermitidos.Add("/swagger/v1/swagger.json");
            valoresPermitidos.Add("/api/auth/login");
            valoresPermitidos.Add("/api/auth/register");
            valoresPermitidos.Add("/api/auth/refresh");
            valoresPermitidos.Add("/swagger/swagger-ui-standalone-preset.js");
            valoresPermitidos.Add("/swagger/favicon-32x32.png");
            valoresPermitidos.Add("/swagger/swagger-ui.css");
            valoresPermitidos.Add("/swagger/swagger-ui-bundle.js");
        }

        public async Task Invoke(HttpContext context, IService<Usuario> userService, IAutorizacionRolPermiso rolPermisoService, IJwtUtils jwtUtils)
        {
            if (!valoresPermitidos.Contains(context.Request.Path.ToString().ToLower()))
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()?? "";
                if (!string.IsNullOrEmpty(token))
                {
                    //Devuelve el userId si el token es válido
                    var userId = jwtUtils.ValidateToken(token ?? "");
                    if (userId != -1)
                    {
                        // obtiene información del usuario
                        var user = await userService.GetById(userId.Value);
                        if (user != null)
                        {
                            string endpoint = context.Request.Path;

                            //validar permiso al endpoint
                            var authorized = rolPermisoService.PermisoEndpoint(endpoint, user.Data.UsuarioId);
                            if (authorized)
                            {
                                //Si está autorizado se guarda el detalle del usuario que se valida en CustomAuthorizeAttribute
                                context.Items["User"] = user.Data;
                            }
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Unauthorized client");
                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token was not provided ");
                    return;
                }


            }

            await _next(context);
        }


    }
}
