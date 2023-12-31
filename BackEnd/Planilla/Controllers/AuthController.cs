﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using Planilla.Services;
using Planilla.Abstractions;
using Planilla.DTO.Others;
using Planilla.Attributes;
using Planilla.DTO;
using Planilla.Entities;
using Planilla.DTO.Auth;
using Planilla.DataAccess;

namespace Planilla.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration _configuration;
        readonly protected AuthService _authService;

        public AuthController(ApiDBContext dBContext, IConfiguration configuration, IAppSettingsModule appSettingsModule, IOptions<AppSettingsJwt> appSettings, IMapper mapper)
        {
            _configuration = configuration;

            _authService = new AuthService(dBContext,appSettingsModule,mapper);
        }

        /// <summary>
        /// Metodo anonimo para realizar logeo en aplicacion angular
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<LoginResponseDTO>> Login(LoginDTO login)
        {
            return await _authService.LoginUsuario(login, _configuration);
        }

        /// <summary>
        /// Metodo anonimo para registrar un nuevo usuario de prueba
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ResponseWrapperDTO<Usuario>> Register(Usuario usuario, int userId)
        {
            return await _authService.RegistrarUsuario(usuario, userId);
        }

        /// <summary>
        /// Refresca token de usuario
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("refresh")]
        public ResponseWrapperDTO<TokenDTO> RefrescarToken ()
        {
            return _authService.RefrescarAccessToken(Request, _configuration);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<Usuario>> Get()
        {
            return await GetCurrentUser();
        }

        /// <summary>
        /// Obtiene el detalle del usuario en el token de la peticion
        /// </summary>
        /// <returns></returns>
        private async Task<ResponseWrapperDTO<Usuario>> GetCurrentUser()
        {
            ResponseWrapperDTO<Usuario> response = new ResponseWrapperDTO<Usuario>();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                int usuarioId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Sid)?.Value);
                if (usuarioId > 0)
                {
                    response = await _authService.GetById(usuarioId);
                }
            }
            return response;
        }
    }
}
