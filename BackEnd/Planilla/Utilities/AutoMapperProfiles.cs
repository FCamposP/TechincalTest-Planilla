using AutoMapper;
using Planilla.DTO;
using Planilla.DTO.Componente;
using Planilla.DTO.ConfiguracionGlobal;
using Planilla.DTO.LogError;
using Planilla.Entities;

namespace Back.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
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
        }
    }
}
