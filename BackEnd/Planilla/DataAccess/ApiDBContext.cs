using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Planilla.Entities;
using System.Linq.Expressions;


// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planilla.DataAccess
{
    public partial class ApiDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApiDBContext()
        {
        }

        public ApiDBContext(DbContextOptions<ApiDBContext> options, IConfiguration configuration)
                : base(options)
        {
            this._configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this._configuration.GetConnectionString("SqlServerQEQDB"));
            }
        }

        public virtual DbSet<ColumnaExcel> ColumnaExcel { get; set; }
        public virtual DbSet<Componente> Componente { get; set; }
        public virtual DbSet<ConfiguracionGlobal> ConfiguracionGlobal { get; set; }
        public virtual DbSet<DetallePlanilla> DetallePlanilla { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EncabezadoPlanilla> EncabezadoPlanilla { get; set; }
        public virtual DbSet<EstadoPlanilla> EstadoPlanilla { get; set; }
        public virtual DbSet<LogError> LogError { get; set; }
        public virtual DbSet<LogEvento> LogEvento { get; set; }
        public virtual DbSet<Notificacion> Notificacion { get; set; }
        public virtual DbSet<Periodo> Periodo { get; set; }
        public virtual DbSet<Puesto> Puesto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolPermiso> RolPermiso { get; set; }
        public virtual DbSet<RolUsuario> RolUsuario { get; set; }
        public virtual DbSet<TipoComponente> TipoComponente { get; set; }
        public virtual DbSet<TipoDato> TipoDato { get; set; }
        public virtual DbSet<TipoNotificacion> TipoNotificacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Implementacion de softdelete, solo utiliza registros "activos en el sistema" (con la propiedad Activo=1)
            var entityTypes = modelBuilder.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                var isActiveProperty = entityType.FindProperty("Activo");
                if (isActiveProperty != null && isActiveProperty.ClrType == typeof(bool?))
                {
                    var entityBuilder = modelBuilder.Entity(entityType.ClrType);
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var methodInfo = typeof(EF).GetMethod(nameof(EF.Property))!.MakeGenericMethod(typeof(bool))!;
                    var efPropertyCall = Expression.Call(null, methodInfo, parameter, Expression.Constant("Activo"));
                    var body = Expression.MakeBinary(ExpressionType.Equal, efPropertyCall, Expression.Constant(true));
                    var expression = Expression.Lambda(body, parameter);
                    entityBuilder.HasQueryFilter(expression);
                }
            }

            modelBuilder.Entity<ColumnaExcel>(entity =>
            {
                entity.HasComment("Catálogo de características que serán investigadas en los Quien es Quien, por ejemplo: Precio, cantidad, etc.");

                entity.Property(e => e.ColumnaExcelId).HasComment("Identificador de caracteristica");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasComment("Descripcion del registro");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre del registro");

                entity.Property(e => e.TipoDatoId).HasComment("Identificador del tipo de Dato");

                entity.HasOne(d => d.TipoDato)
                    .WithMany(p => p.ColumnaExcel)
                    .HasForeignKey(d => d.TipoDatoId)
                    .HasConstraintName("FK_COLUMNAE_REFERENCE_TIPODATO");
            });

            modelBuilder.Entity<Componente>(entity =>
            {
                entity.HasComment("Instancias de los tipos de componentes a utilizar en el sistema.");

                entity.Property(e => e.ComponenteId).HasComment("Identificador de componente");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasComment("Descripcion del registro");

                entity.Property(e => e.Icon)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Icono del componente, si fuese necesario");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre del registro");

                entity.Property(e => e.NombreMostrar)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre a mostrar del componente en pantalla");

                entity.Property(e => e.Orden).HasComment("Indica el orden o posicion en que se deben de listar los componentes segun su tipo y rol");

                entity.Property(e => e.PadreId).HasComment("Identificador del componente padre");

                entity.Property(e => e.TipoComponenteId).HasComment("Identificador de tipo de componente");

                entity.Property(e => e.Url)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Ruta de endpoint si fuese requerido por el componente");

                entity.HasOne(d => d.Padre)
                    .WithMany(p => p.InversePadre)
                    .HasForeignKey(d => d.PadreId)
                    .HasConstraintName("FK_Componente_Componente");

                entity.HasOne(d => d.TipoComponente)
                    .WithMany(p => p.Componente)
                    .HasForeignKey(d => d.TipoComponenteId)
                    .HasConstraintName("FK_Componente_TipoComponente");
            });

            modelBuilder.Entity<ConfiguracionGlobal>(entity =>
            {
                entity.HasKey(e => e.ConfiguracionId)
                    .HasName("PK_CONFIGURACIONGLOBAL");

                entity.HasComment(@"Tabla de configuraciones generales del sistema Quien es Quien de los Precios.
   Cada registro de esta tabla debe de ser análoga a las propiedades de la clase ConfiguracionGlobal en la carpeta Settings en Utilities");

                entity.Property(e => e.ConfiguracionId).HasComment("Identificador de configuracion");

                entity.Property(e => e.Activo)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Codigo de configuracion, nombre de propiedad de archivo de configuracion");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.NombreAmostrar)
                    .HasColumnName("NombreAMostrar")
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre a ser mostrado en pantallas de usuario");

                entity.Property(e => e.Valor)
                    .IsUnicode(false)
                    .HasComment("Máximo de intentos fallidos en inicio de sesion de los usuarios");
            });

            modelBuilder.Entity<DetallePlanilla>(entity =>
            {
                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creador).HasDefaultValueSql("((1))");

                entity.Property(e => e.DescuentoAfp)
                    .HasColumnName("DescuentoAFP")
                    .HasColumnType("money");

                entity.Property(e => e.DescuentoIsss)
                    .HasColumnName("DescuentoISSS")
                    .HasColumnType("money");

                entity.Property(e => e.DescuentoRenta).HasColumnType("money");

                entity.Property(e => e.EmpleadoId).HasComment("Identificador de un empleado");

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.OtrosDescuentos).HasColumnType("money");

                entity.Property(e => e.Salario).HasColumnType("money");

                entity.Property(e => e.SueldoNeto).HasColumnType("money");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.DetallePlanilla)
                    .HasForeignKey(d => d.EmpleadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLEP_REFERENCE_EMPLEADO");

                entity.HasOne(d => d.EncabezadoPlanilla)
                    .WithMany(p => p.DetallePlanilla)
                    .HasForeignKey(d => d.EncabezadoPlanillaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLEP_REFERENCE_ENCABEZA");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasComment("Catálogo de Empleados de la institución que harán uso del sistema Quien es Quien.");

                entity.Property(e => e.EmpleadoId).HasComment("Identificador de un empleado");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Correo electrónico de empleado");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Primer apellido del empleado");

                entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Primer nombre del empleado");

                entity.Property(e => e.PuestoId).HasComment("Identificador de un puesto de trabajo");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Segundo apellido del empleado");

                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Segundo nombre del empleado");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Telefono de contacto del empleado");

                entity.HasOne(d => d.Puesto)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.PuestoId)
                    .HasConstraintName("FK_Empleado_Puesto");
            });

            modelBuilder.Entity<EncabezadoPlanilla>(entity =>
            {
                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.CorreoEnviado).HasDefaultValueSql("((0))");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creador).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EnviarCorreo).HasDefaultValueSql("((0))");

                entity.Property(e => e.FechaCorte).HasColumnType("datetime");

                entity.Property(e => e.Habilitado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.PeriodoId).HasComment("Identificador del periodo");

                entity.HasOne(d => d.EstadoPlanilla)
                    .WithMany(p => p.EncabezadoPlanilla)
                    .HasForeignKey(d => d.EstadoPlanillaId)
                    .HasConstraintName("FK_ENCABEZA_REFERENCE_ESTADOPL");

                entity.HasOne(d => d.Periodo)
                    .WithMany(p => p.EncabezadoPlanilla)
                    .HasForeignKey(d => d.PeriodoId)
                    .HasConstraintName("FK_ENCABEZA_REFERENCE_PERIODO");
            });

            modelBuilder.Entity<EstadoPlanilla>(entity =>
            {
                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creador).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogError>(entity =>
            {
                entity.HasComment("Registros de errores ocurridos en el sistema.");

                entity.Property(e => e.LogErrorId).HasComment("Identificador del registro de error");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Entorno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Indica el entorno de ejecución (DEV,QA,PRD)");

                entity.Property(e => e.Hresult)
                    .HasColumnName("HResult")
                    .HasComment("HResult ocurrido en una excepción de sistema");

                entity.Property(e => e.InformacionAdicional)
                    .IsUnicode(false)
                    .HasComment("Información adicional de un error");

                entity.Property(e => e.InnerExceptionHresult)
                    .HasColumnName("InnerExceptionHResult")
                    .HasComment("Valor de InnerExceptionHResult ocurrido en una excepción");

                entity.Property(e => e.InnerExceptionMessage)
                    .IsUnicode(false)
                    .HasComment("Valor de InnerMensajeExcepcion ocurrido en una excepcion");

                entity.Property(e => e.InnerExceptionSource)
                    .IsUnicode(false)
                    .HasComment("Valor de InnerOrigenException ocurrido en una excepcin");

                entity.Property(e => e.InnerExceptionStackTrace)
                    .IsUnicode(false)
                    .HasComment("Valor de InnerExceptionStackTrace ocurrido en una excepcion");

                entity.Property(e => e.InnerExceptionTargetSite)
                    .IsUnicode(false)
                    .HasComment("Valor de InnerExceptionTargetSite ocurrido en una excepcion");

                entity.Property(e => e.IpOrigen)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasComment("Ip del lugar en que ocurrió el evento");

                entity.Property(e => e.Message)
                    .IsUnicode(false)
                    .HasComment("Mensaje de la excepción");

                entity.Property(e => e.Modulo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Codigo del modulo en que ocurrio la excepcion");

                entity.Property(e => e.Source)
                    .IsUnicode(false)
                    .HasComment("Descripcion de donde se origino la excepcion");

                entity.Property(e => e.StackTrace)
                    .IsUnicode(false)
                    .HasComment("Valor de StackTrace ocurrido en una excepcion");

                entity.Property(e => e.TargetSite)
                    .IsUnicode(false)
                    .HasComment("Valor de TargetSite ocurrido en una excepcion");
            });

            modelBuilder.Entity<LogEvento>(entity =>
            {
                entity.HasComment("Registros de acciones realizadas por cada usuario en el sistema.");

                entity.Property(e => e.LogEventoId).HasComment("Identificador del registro de evento");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Entorno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Indica el entorno de ejecución (DEV,QA,PRD)");

                entity.Property(e => e.IpOrigen)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasComment("Ip del lugar en que ocurrió el error");

                entity.Property(e => e.Mensaje)
                    .IsUnicode(false)
                    .HasComment("Descripcion del registro");

                entity.Property(e => e.Modulo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Codigo identificador de log de eventos");

                entity.Property(e => e.TipoLog)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Tipo de Log");
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creador).HasDefaultValueSql("((1))");

                entity.Property(e => e.Mensaje)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId).HasComment("Identificador de usuario");

                entity.HasOne(d => d.TipoNotificacion)
                    .WithMany(p => p.Notificacion)
                    .HasForeignKey(d => d.TipoNotificacionId)
                    .HasConstraintName("FK_NOTIFICA_REFERENCE_TIPONOTI");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Notificacion)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_NOTIFICA_REFERENCE_USUARIO");
            });

            modelBuilder.Entity<Periodo>(entity =>
            {
                entity.HasComment("Periodos de un Quien es Quien, periodo de Vigencia a ser mostrado, y periodo que duró la creación del Quien es Quien");

                entity.Property(e => e.PeriodoId).HasComment("Identificador del periodo");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasComment("Fecha final de un periodo");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasComment("Fecha inicio de un periodo");

                entity.Property(e => e.Habilitado).HasComment("Identifica al periodo válido en curso, debe haber solo 1 registro habilitado");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.HasComment("Catálogo de puestos de trabajos.");

                entity.Property(e => e.PuestoId).HasComment("Identificador de un puesto de trabajo");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasComment("Descripcion del registro");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre del registro");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasComment("Catálogo de tipo de perfiles que contienen un conjunto de permisos para asignar a un usuario.");

                entity.Property(e => e.RolId).HasComment("Identificador de un rol de usuario");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasComment("Descripcion del registro");

                entity.Property(e => e.EsSuperUsuario).HasComment("Indica si el rol es para super usuario");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre del registro");
            });

            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.HasComment("Guarda las relaciones de los permisos a los componentes que tendrá cada rol de usuario.");

                entity.Property(e => e.RolPermisoId).HasComment("Identificador de relación rol y permiso");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.ComponenteId).HasComment("Identificador de componente");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.RolId).HasComment("Identificador de un rol de usuario");

                entity.HasOne(d => d.Componente)
                    .WithMany(p => p.RolPermiso)
                    .HasForeignKey(d => d.ComponenteId)
                    .HasConstraintName("FK_RolPermiso_Componente");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolPermiso)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_RolPermiso_Rol");
            });

            modelBuilder.Entity<RolUsuario>(entity =>
            {
                entity.HasComment("Indica los roles que puede tener asociados un usuario.");

                entity.Property(e => e.RolUsuarioId).HasComment("Identificador de rol y usuario");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.RolId).HasComment("Identificador de un rol de usuario");

                entity.Property(e => e.UsuarioId).HasComment("Identificador de Usuario");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolUsuario)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_RolUsuario_Rol");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.RolUsuario)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_RolUsuario_Usuario");
            });

            modelBuilder.Entity<TipoComponente>(entity =>
            {
                entity.HasComment("Catálogo de tipo de componentes a utilizar en el sistema, por ejemplo, vista, vista parcial, endpoint, etc.");

                entity.Property(e => e.TipoComponenteId).HasComment("Identificador de tipo de componente");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Codigo distintivo del registro");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasComment("Descripcion del registro");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre del registro");
            });

            modelBuilder.Entity<TipoDato>(entity =>
            {
                entity.HasComment("Tipo de dato de una caracteristica de QEQ");

                entity.Property(e => e.TipoDatoId).HasComment("Identificador del Tipo de Dato");

                entity.Property(e => e.Activo)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Codigo distintivo del registro");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador).HasComment("Id de usuario creador del registro");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("Detalles sobre el tipo de dato");

                entity.Property(e => e.Formato)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Expresiones regulares para validar el tipo de dato");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Nombre del Tipo de Dato");
            });

            modelBuilder.Entity<TipoNotificacion>(entity =>
            {
                entity.Property(e => e.TipoNotificacionId).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('datetime()')");

                entity.Property(e => e.Creador).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasComment("Catálogo de usuarios que utilizarán el sistema");

                entity.Property(e => e.UsuarioId).HasComment("Identificador de usuario");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("Indica si un registro esta activo en el sistema");

                entity.Property(e => e.Creado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha y hora de creacion del registro");

                entity.Property(e => e.Creador)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Id de usuario creador del registro");

                entity.Property(e => e.EmpleadoId).HasComment("Identificador de un empleado");

                entity.Property(e => e.IntentosFallidos).HasComment("Cantidad de intentos fallidos de cada inicio de sesion de un usuario");

                entity.Property(e => e.Modificado)
                    .HasColumnType("datetime")
                    .HasComment("Fecha y hora de ultima modificacion del registro");

                entity.Property(e => e.Modificador).HasComment("Id del usuario que realizo la última modificacion del registro");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasComment("Contraseña cifrada del usuario");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre de usuario");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.EmpleadoId)
                    .HasConstraintName("FK_Usuario_Empleado");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
