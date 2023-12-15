using Back.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using Planilla.Abstractions;
using Planilla.Attributes;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.Entities;
using Planilla.Services;
using Planilla.Services.LogCustom;
using Planilla.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Planilla
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Mapeo de configuracion del modulo configurado en appsetting.json
            services.Configure<ConfiguracionModulo>(Configuration.GetSection("ConfiguracionModulo"));
            //Mapeo de configuración para JWT en appsetting.json
            services.Configure<AppSettingsJwt>(Configuration.GetSection("Jwt"));
            services.AddControllers(config =>
            {
                //config.Filters.Add(new CustomAuthorizeAttribute());
            });
            services.AddSwaggerGen();

            //Configuracion de AutoMapper para casteo automático entre Entidades y DTO
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            //Obtiene cadena de conexión a base de datos configurada en appsetting.json
            var connectionString = Configuration.GetConnectionString("SqlServerQEQDB");

            //Implementacion de softdelete en ApiDBContext
            services.AddDbContext<ApiDBContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<Microsoft.EntityFrameworkCore.DbContext, ApiDBContext>();

            services.AddScoped<IService<Usuario>, AuthService>();
            services.AddScoped<IAutorizacionRolPermiso, RolPermisoService>();
            services.AddScoped<ILogErrorger, DBLogErrorger>();
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IAppSettingsModule, AppSettingsModule>();

            //Configuración de Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Planilla Fc",
                    Description = "API de planilla de pagos",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Nombre Empresa",
                        Url = new Uri("https://example.com/contact")
                    },
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Nuestra Licencia",
                    //    Url = new Uri("https://example.com/license")
                    //}
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
            });

            //Configuracion de autenticacion JWT 

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });


            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            // Configure the HTTP request pipeline.

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        }
    }
}
