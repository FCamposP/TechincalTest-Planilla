using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Planilla.Abstractions;
using Planilla.DTO;
using Planilla.DTO.Auth;
using Planilla.DTO.Others;
using Planilla.Services.LogCustom;
using Planilla.Utilities;
using Planilla.Entities;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Planilla.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Planilla.Services
{
    public class AuthService : BaseService<Usuario>
    {
        private readonly EmpleadoService _empleadoService;

        public AuthService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _empleadoService = new EmpleadoService(context, appSettingsModule, mapper);
        }

        public async Task<ResponseWrapperDTO<Usuario>> RegistrarUsuario(Usuario usuario, int userId)
        {
            ResponseWrapperDTO<Usuario> response = new ResponseWrapperDTO<Usuario>();
            if (usuario != null)
            {
                usuario.Activo = true;
                var encription = await _dBContext.ConfiguracionGlobal.Where(x => x.Codigo == "ENCRIPTIONKEY").FirstOrDefaultAsync();
                usuario.Password = AesManaged.Encrypt(usuario.Password ?? "", encription.Valor);
                response = await Crear(usuario, userId);
            }
            return response;
        }
        public async Task<ResponseWrapperDTO<LoginResponseDTO>> LoginUsuario(LoginDTO login, IConfiguration _configuration)
        {
            ResponseWrapperDTO<LoginResponseDTO> response = new ResponseWrapperDTO<LoginResponseDTO>();
            if (login != null)
            {
                var usuario = this.Autenticar(login);
                if (usuario != null)
                {
                    TokenDTO tokenResponse = await this.GenerarToken(usuario, _configuration);
                    LoginResponseDTO responseDTO = new LoginResponseDTO(tokenResponse, usuario.UsuarioId);
                    response.Data = responseDTO;
                }
            }
            return response;
        }

        private Usuario Autenticar(LoginDTO login)
        {
            Usuario? user = null;
            if (login != null)
            {
                var encription = _dBContext.ConfiguracionGlobal.Where(x => x.Codigo == "ENCRIPTIONKEY").FirstOrDefault();

                user = _items.Where(x => x.UserName.ToLower() == login.Username.ToLower()
                && x.Password == AesManaged.Encrypt(login.Password, encription.Valor) && x.Activo == true).FirstOrDefault();
            }
            return user;
        }

        private async Task<TokenDTO> GenerarToken(Usuario usuario, IConfiguration _config)
        {

            try
            {
                TokenDTO tokenResponse = new TokenDTO();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var empleadoResponse = await _empleadoService.GetById(usuario.EmpleadoId ?? -1);

                if (empleadoResponse.Data == null)
                {
                    empleadoResponse.Data = new Empleado();
                }
                //seteo de claims
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, usuario.UserName??""),
                new Claim(ClaimTypes.Email,empleadoResponse.Data.Email),
                new Claim(ClaimTypes.GivenName, empleadoResponse.Data.PrimerNombre??""),
                new Claim(ClaimTypes.Surname, empleadoResponse.Data.PrimerApellido??""),
                new Claim("Id" , Convert.ToString(usuario.UsuarioId))
            };
                //hacer parametrizable el tiempo de validez del token
                DateTime expireAt = DateTime.Now.AddMinutes(10);
                DateTime dateTime = DateTime.ParseExact(expireAt.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime utcDateTime = dateTime.ToUniversalTime();
                double timestamp = (utcDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                int expireAtNumber = (int)timestamp;

                var accessToken = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: expireAt,
                    signingCredentials: credentials);

                var refreshToken = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(240),
                    signingCredentials: credentials);

                tokenResponse.AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
                tokenResponse.RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
                tokenResponse.ExpireAt = expireAtNumber;

                return tokenResponse;
            }
            catch (Exception ex)
            {

                throw;
            }
        
        }

        public ResponseWrapperDTO<TokenDTO>? RefrescarAccessToken(HttpRequest request, IConfiguration _config)
        {
            try
            {
                ResponseWrapperDTO<TokenDTO> response = new ResponseWrapperDTO<TokenDTO>();
                string refreshToken = request.Headers["Refresh_token"]!;

                if (refreshToken == null || refreshToken == "")
                {
                    throw new UnauthorizedAccessException("El refresh token ha expirado.");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["Jwt:Audience"],
                    ValidateLifetime = false, // Validar la expiración del token
                    ClockSkew = TimeSpan.Zero // Desactivar el tiempo de tolerancia para una expiración precisa
                };

                var principal = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out SecurityToken validatedToken);
                var jwtSecurityToken = validatedToken as JwtSecurityToken;

                // Obtener la fecha de expiración del token
                var expires = jwtSecurityToken?.ValidTo;

                if (expires.HasValue && expires.Value < DateTime.UtcNow)
                {
                    throw new UnauthorizedAccessException("El refresh token ha expirado.");
                }

                // Obtener los claims del token de refresco
                var userName = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var firstName = principal.FindFirst(ClaimTypes.GivenName)?.Value;
                var lastName = principal.FindFirst(ClaimTypes.Surname)?.Value;
                var userId = principal.FindFirst("Id")?.Value;

                // Crear nuevos claims
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName!),
                    new Claim(ClaimTypes.Email, email!),
                    new Claim(ClaimTypes.GivenName, firstName!),
                    new Claim(ClaimTypes.Surname, lastName!),
                    new Claim("Id", userId!)
                };

                // Generar el nuevo access token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                DateTime expireAt = DateTime.Now.AddMinutes(10);
                DateTime dateTime = DateTime.ParseExact(expireAt.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime utcDateTime = dateTime.ToUniversalTime();
                double timestamp = (utcDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                int expireAtNumber = (int)timestamp;

                var accessToken = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: expireAt,
                    signingCredentials: credentials);

                var newAccessToken = tokenHandler.WriteToken(accessToken);

                TokenDTO tokenResponse = new TokenDTO
                {
                    AccessToken = newAccessToken,
                    ExpireAt = expireAtNumber
                };

                response.Data = tokenResponse;

                return response;
            }
            catch(UnauthorizedAccessException ex)
            {
                var unauthorizedResponse = new ResponseWrapperDTO<TokenDTO>
                {
                    Status = new GlobalStatusDTO
                    {
                        RequestStatus = new RequestStatusDTO
                        {
                            Codigo = 401,
                            Mensaje = "Unauthorized"
                        },
                        ResponseStatus = new ResponseStatusDTO
                        {
                            Codigo = 0,
                            Mensaje = ex.GetType().ToString(),
                            MensajeError = ex.Message
                        }
                    }
                };

                return unauthorizedResponse;
            }

        }


    }
}
