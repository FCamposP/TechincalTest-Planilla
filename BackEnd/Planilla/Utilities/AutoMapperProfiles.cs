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
                .ForMember(x => x.TipoComponente, opt => opt.Ignore())
                .ForMember(dest => dest.TipoComponente, opt => opt.MapFrom(src => src.TipoComponente!=null? src.TipoComponente.Nombre:""))
                .ForMember(dest => dest.NombrePadre, opt => opt.MapFrom(src => src.Padre!=null? src.Padre.NombreMostrar:"")).ReverseMap();
            CreateMap<TipoComponente, TipoComponenteDTO>().ReverseMap();
            CreateMap<RolDTO, Rol>()
                .ForMember(x => x.RolUsuario, opt => opt.Ignore())
                .ForMember(x => x.RolPermiso, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<RolUsuario, RolUsuarioDTO>().ReverseMap();
            CreateMap<RolPermiso, RolPermisoDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>()
                 .ForMember(dest => dest.EmpleadoNombre, opt => opt.MapFrom(src => (src.Empleado!=null? src.Empleado.PrimerNombre + " " + src.Empleado.PrimerApellido:"")))
                .ReverseMap();
            CreateMap<ConfiguracionGlobal, ConfiguracionGlobalDTO>().ReverseMap();

            CreateMap<LogError, LogErrorDTO>().ReverseMap();
            CreateMap<LogError, LogErrorSimpleDTO>().ReverseMap();
        }
    }
}
    