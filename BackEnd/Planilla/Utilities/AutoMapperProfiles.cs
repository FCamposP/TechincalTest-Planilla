using AutoMapper;
using Planilla.DTO;
using Planilla.DTO.Componente;
using Planilla.DTO.ConfiguracionGlobal;
using Planilla.DTO.LogError;
using Planilla.DTO.Planilla;
using Planilla.Entities;
using System;

namespace Back.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        /// <summary>
        /// Mapeo automatico entre entidades y DTO
        /// </summary>
        public AutoMapperProfiles()
        {
            CreateMap<Componente, ComponenteDTO>()
                .ForMember(dest => dest.NombreTipoComponente, opt => opt.MapFrom(src => src.TipoComponente != null ? src.TipoComponente.Nombre : ""))
                .ForMember(dest => dest.NombrePadre, opt => opt.MapFrom(src => src.Padre != null ? src.Padre.NombreMostrar : ""));
            CreateMap<ComponenteDTO, Componente>();
            CreateMap<TipoComponente, TipoComponenteDTO>().ReverseMap();
            CreateMap<RolDTO, Rol>()
                .ForMember(x => x.RolUsuario, opt => opt.Ignore())
                .ForMember(x => x.RolPermiso, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<RolUsuario, RolUsuarioDTO>().ReverseMap();
            CreateMap<RolPermiso, RolPermisoDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>()
                 .ForMember(dest => dest.EmpleadoNombre, opt => opt.MapFrom(src => (src.Empleado != null ? src.Empleado.PrimerNombre + " " + src.Empleado.PrimerApellido : "")))
                .ReverseMap();
            CreateMap<ConfiguracionGlobal, ConfiguracionGlobalDTO>().ReverseMap();
            CreateMap<Empleado, EmpleadoDTO>()
             .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.PrimerNombre + " " + src.PrimerApellido))
             .ForMember(dest => dest.NombrePuesto, opt => opt.MapFrom(src => src.Puesto != null ? src.Puesto.Nombre : ""))
            .ReverseMap();
            CreateMap<Periodo, PeriodoDTO>()
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.FechaInicio.Value.ToString("dd/MM/yyy") + " - " + src.FechaFin.Value.ToString("dd/MM/yyy")))
                .ReverseMap();
            CreateMap<LogError, LogErrorDTO>().ReverseMap();
            CreateMap<LogError, LogErrorSimpleDTO>();
            CreateMap<Puesto, PuestoDTO>().ReverseMap();
            CreateMap<TipoDato, TipoDatoDTO>().ReverseMap();
            CreateMap<ColumnaExcel, ColumnaExcelDTO>()
             .ForMember(dest => dest.NombreTipoDato, opt => opt.MapFrom(src => src.TipoDato != null ? src.TipoDato.Nombre : ""));
            CreateMap<ColumnaExcelDTO, ColumnaExcel>();
            CreateMap<EstadoPlanilla, EstadoPlanillaDTO>();
            CreateMap<EncabezadoPlanillaDTO, EncabezadoPlanilla>();
            CreateMap<EncabezadoPlanilla, EncabezadoPlanillaDTO>()
                .ForMember(x => x.DetallePlanilla, opt => opt.Ignore())
                .ForMember(dest => dest.NombreEstado, opt => opt.MapFrom(src => src.EstadoPlanilla != null ? src.EstadoPlanilla.Nombre : ""))
                .ForMember(dest => dest.DescripcionPeriodo, opt => opt.MapFrom(src => src.Periodo.FechaInicio.Value.ToString("dd/MM/yyy") + " - " + src.Periodo.FechaFin.Value.ToString("dd/MM/yyy")))
                .ReverseMap();
            CreateMap<DetallePlanilla, DetallePlanillaDTO>()
                .ForMember(dest => dest.CodigoEmpleado, opt => opt.MapFrom(src => src.Empleado != null ? src.Empleado.Codigo : ""))
                .ReverseMap();
            CreateMap<DetallePlanilla, ResumenBoletaPagoDTO>()
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.EncabezadoPlanilla != null ? src.EncabezadoPlanilla.Descripcion : ""))
                .ForMember(dest => dest.FechaCorte, opt => opt.MapFrom(src => src.EncabezadoPlanilla.Periodo != null ? src.EncabezadoPlanilla.Periodo.FechaFin.Value.Date.ToString("yyyy-MM-dd") : ""));
            CreateMap<DetallePlanilla, BoletaPagoDTO>()
                           .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.EncabezadoPlanilla != null ? src.EncabezadoPlanilla.Descripcion : ""))
                           .ForMember(dest => dest.FechaCorte, opt => opt.MapFrom(src => src.EncabezadoPlanilla.Periodo != null ? src.EncabezadoPlanilla.Periodo.FechaFin.Value.Date.ToString("yyyy-MM-dd") : ""))
                           .ForMember(dest => dest.CodigoEmpleado, opt => opt.MapFrom(src => src.Empleado != null ? src.Empleado.Codigo: ""))
                           .ForMember(dest => dest.NombreEmpleado, opt => opt.MapFrom(src => src.Empleado != null ? (src.Empleado.PrimerNombre+" "+src.Empleado.SegundoNombre+" "+src.Empleado.PrimerNombre+" "+src.Empleado.SegundoApellido) : ""))
                           .ForMember(dest => dest.NombrePuesto, opt => opt.MapFrom(src => src.Empleado != null ? src.Empleado.Puesto.Nombre : ""));

        }
    }
}
