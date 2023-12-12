/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     12/12/2023 3:37:23 PM                        */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ColumnaExcel') and o.name = 'FK_COLUMNAE_REFERENCE_TIPODATO')
alter table ColumnaExcel
   drop constraint FK_COLUMNAE_REFERENCE_TIPODATO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Componente') and o.name = 'FK_Componente_Componente')
alter table Componente
   drop constraint FK_Componente_Componente
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Componente') and o.name = 'FK_Componente_TipoComponente')
alter table Componente
   drop constraint FK_Componente_TipoComponente
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DetallePlanilla') and o.name = 'FK_DETALLEP_REFERENCE_ENCABEZA')
alter table DetallePlanilla
   drop constraint FK_DETALLEP_REFERENCE_ENCABEZA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Empleado') and o.name = 'FK_Empleado_Puesto')
alter table Empleado
   drop constraint FK_Empleado_Puesto
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EncabezadoPlanilla') and o.name = 'FK_ENCABEZA_REFERENCE_PERIODO')
alter table EncabezadoPlanilla
   drop constraint FK_ENCABEZA_REFERENCE_PERIODO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EncabezadoPlanilla') and o.name = 'FK_ENCABEZA_REFERENCE_ESTADOPL')
alter table EncabezadoPlanilla
   drop constraint FK_ENCABEZA_REFERENCE_ESTADOPL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Notificacion') and o.name = 'FK_NOTIFICA_REFERENCE_USUARIO')
alter table Notificacion
   drop constraint FK_NOTIFICA_REFERENCE_USUARIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Notificacion') and o.name = 'FK_NOTIFICA_REFERENCE_TIPONOTI')
alter table Notificacion
   drop constraint FK_NOTIFICA_REFERENCE_TIPONOTI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RolPermiso') and o.name = 'FK_RolPermiso_Componente')
alter table RolPermiso
   drop constraint FK_RolPermiso_Componente
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RolPermiso') and o.name = 'FK_RolPermiso_Rol')
alter table RolPermiso
   drop constraint FK_RolPermiso_Rol
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RolUsuario') and o.name = 'FK_RolUsuario_Rol')
alter table RolUsuario
   drop constraint FK_RolUsuario_Rol
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RolUsuario') and o.name = 'FK_RolUsuario_Usuario')
alter table RolUsuario
   drop constraint FK_RolUsuario_Usuario
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Usuario') and o.name = 'FK_Usuario_Empleado')
alter table Usuario
   drop constraint FK_Usuario_Empleado
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ColumnaExcel')
            and   type = 'U')
   drop table ColumnaExcel
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Componente')
            and   type = 'U')
   drop table Componente
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ConfiguracionGlobal')
            and   type = 'U')
   drop table ConfiguracionGlobal
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DetallePlanilla')
            and   type = 'U')
   drop table DetallePlanilla
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Empleado')
            and   type = 'U')
   drop table Empleado
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EncabezadoPlanilla')
            and   type = 'U')
   drop table EncabezadoPlanilla
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EstadoPlanilla')
            and   type = 'U')
   drop table EstadoPlanilla
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LogError')
            and   type = 'U')
   drop table LogError
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LogEvento')
            and   type = 'U')
   drop table LogEvento
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Notificacion')
            and   type = 'U')
   drop table Notificacion
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Periodo')
            and   type = 'U')
   drop table Periodo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Puesto')
            and   type = 'U')
   drop table Puesto
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Rol')
            and   type = 'U')
   drop table Rol
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RolPermiso')
            and   type = 'U')
   drop table RolPermiso
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RolUsuario')
            and   type = 'U')
   drop table RolUsuario
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TipoComponente')
            and   type = 'U')
   drop table TipoComponente
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TipoDato')
            and   type = 'U')
   drop table TipoDato
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TipoNotificacion')
            and   type = 'U')
   drop table TipoNotificacion
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Usuario')
            and   type = 'U')
   drop table Usuario
go

/*==============================================================*/
/* Table: ColumnaExcel                                          */
/*==============================================================*/
create table ColumnaExcel (
   ColumnaExcelId       int                  identity,
   TipoDatoId           int                  null,
   Nombre               varchar(256)         not null,
   Descripcion          text                 null,
   Orden                int                  null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_COLUMNAEXCEL primary key (ColumnaExcelId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ColumnaExcel') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'ColumnaExcel' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Catálogo de características que serán investigadas en los Quien es Quien, por ejemplo: Precio, cantidad, etc.', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ColumnaExcel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColumnaExcelId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'ColumnaExcelId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de caracteristica',
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'ColumnaExcelId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ColumnaExcel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TipoDatoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'TipoDatoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador del tipo de Dato',
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'TipoDatoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ColumnaExcel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Nombre')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Nombre'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Nombre del registro',
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Nombre'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ColumnaExcel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Descripcion')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Descripcion'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Descripcion del registro',
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Descripcion'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ColumnaExcel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ColumnaExcel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ColumnaExcel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ColumnaExcel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ColumnaExcel')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'ColumnaExcel', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: Componente                                            */
/*==============================================================*/
create table Componente (
   ComponenteId         int                  identity,
   TipoComponenteId     int                  null,
   PadreId              int                  null,
   Nombre               varchar(256)         not null,
   NombreMostrar        varchar(256)         null,
   Descripcion          text                 null,
   Orden                int                  null,
   Url                  varchar(256)         null,
   Icon                 varchar(256)         null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_COMPONENTE primary key (ComponenteId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Componente') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'Componente' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Instancias de los tipos de componentes a utilizar en el sistema.', 
   'schema', @CurrentUser, 'table', 'Componente'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ComponenteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'ComponenteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de componente',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'ComponenteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TipoComponenteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'TipoComponenteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de tipo de componente',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'TipoComponenteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PadreId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'PadreId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador del componente padre',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'PadreId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Nombre')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Nombre'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Nombre del registro',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Nombre'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NombreMostrar')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'NombreMostrar'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Nombre a mostrar del componente en pantalla',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'NombreMostrar'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Descripcion')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Descripcion'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Descripcion del registro',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Descripcion'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Orden')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Orden'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica el orden o posicion en que se deben de listar los componentes segun su tipo y rol',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Orden'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Url')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Url'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Ruta de endpoint si fuese requerido por el componente',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Url'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Icon')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Icon'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Icono del componente, si fuese necesario',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Icon'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Componente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'Componente', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: ConfiguracionGlobal                                   */
/*==============================================================*/
create table ConfiguracionGlobal (
   ConfiguracionId      int                  identity,
   Codigo               varchar(256)         not null,
   Valor                varchar(Max)         null,
   NombreAMostrar       varchar(256)         null,
   Activo               bit                  null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_CONFIGURACIONGLOBAL primary key (ConfiguracionId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ConfiguracionGlobal') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Tabla de configuraciones generales del sistema Quien es Quien de los Precios.
   Cada registro de esta tabla debe de ser análoga a las propiedades de la clase ConfiguracionGlobal en la carpeta Settings en Utilities', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ConfiguracionGlobal')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ConfiguracionId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'ConfiguracionId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de configuracion',
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'ConfiguracionId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ConfiguracionGlobal')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Codigo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Codigo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Codigo de configuracion, nombre de propiedad de archivo de configuracion',
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Codigo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ConfiguracionGlobal')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Valor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Valor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Máximo de intentos fallidos en inicio de sesion de los usuarios',
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Valor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ConfiguracionGlobal')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NombreAMostrar')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'NombreAMostrar'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Nombre a ser mostrado en pantallas de usuario',
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'NombreAMostrar'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ConfiguracionGlobal')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ConfiguracionGlobal')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ConfiguracionGlobal')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ConfiguracionGlobal')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ConfiguracionGlobal')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'ConfiguracionGlobal', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: DetallePlanilla                                       */
/*==============================================================*/
create table DetallePlanilla (
   DetallePlanillaId    int                  identity,
   EncabezadoPlanillaId int                  null,
   FechaCorte           datetime             not null,
   DescuentoISSS        money                not null,
   DescuentoAFP         money                not null,
   DescuentoRenta       money                not null,
   DescuentoOtros       money                not null,
   SueldoNeto           money                null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_DETALLEPLANILLA primary key (DetallePlanillaId)
)
go

/*==============================================================*/
/* Table: Empleado                                              */
/*==============================================================*/
create table Empleado (
   EmpleadoId           int                  identity,
   PuestoId             int                  null,
   PrimerNombre         varchar(100)         null,
   SegundoNombre        varchar(100)         null,
   PrimerApellido       varchar(100)         null,
   SegundoApellido      varchar(100)         null,
   Email                varchar(256)         not null,
   Telefono             varchar(256)         null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_EMPLEADO primary key (EmpleadoId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Empleado') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'Empleado' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Catálogo de Empleados de la institución que harán uso del sistema Quien es Quien.', 
   'schema', @CurrentUser, 'table', 'Empleado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EmpleadoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'EmpleadoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de un empleado',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'EmpleadoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PuestoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'PuestoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de un puesto de trabajo',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'PuestoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrimerNombre')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'PrimerNombre'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Primer nombre del empleado',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'PrimerNombre'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SegundoNombre')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'SegundoNombre'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Segundo nombre del empleado',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'SegundoNombre'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrimerApellido')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'PrimerApellido'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Primer apellido del empleado',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'PrimerApellido'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SegundoApellido')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'SegundoApellido'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Segundo apellido del empleado',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'SegundoApellido'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Email')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Email'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Correo electrónico de empleado',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Email'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Telefono')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Telefono'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Telefono de contacto del empleado',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Telefono'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Empleado')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'Empleado', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: EncabezadoPlanilla                                    */
/*==============================================================*/
create table EncabezadoPlanilla (
   EncabezadoPlanillaId int                  identity,
   PeriodoId            int                  null,
   EstadoPlanillaId     int                  null,
   Descripcion          varchar(500)         not null,
   EnviarCorreo         bit                  null default 0,
   CorreoEnviado        bit                  null default 0,
   Activo               bit                  null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default GETDATE(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_ENCABEZADOPLANILLA primary key (EncabezadoPlanillaId)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EncabezadoPlanilla')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PeriodoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'EncabezadoPlanilla', 'column', 'PeriodoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador del periodo',
   'schema', @CurrentUser, 'table', 'EncabezadoPlanilla', 'column', 'PeriodoId'
go

/*==============================================================*/
/* Table: EstadoPlanilla                                        */
/*==============================================================*/
create table EstadoPlanilla (
   EstadoPlanillaId     int                  identity,
   Codigo               varchar(10)          not null,
   Nombre               varchar(250)         not null,
   Descripcion          varchar(500)         null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_ESTADOPLANILLA primary key (EstadoPlanillaId)
)
go

/*==============================================================*/
/* Table: LogError                                              */
/*==============================================================*/
create table LogError (
   LogErrorId           int                  identity,
   Modulo               varchar(100)         null,
   Entorno              varchar(100)         null,
   IpOrigen             varchar(25)          null,
   InformacionAdicional varchar(Max)         null,
   HResult              int                  null,
   Message              varchar(Max)         null,
   StackTrace           varchar(Max)         null,
   Source               varchar(Max)         null,
   TargetSite           varchar(Max)         null,
   InnerExceptionHResult int                  null,
   InnerExceptionMessage varchar(Max)         null,
   InnerExceptionSource varchar(Max)         null,
   InnerExceptionStackTrace varchar(Max)         null,
   InnerExceptionTargetSite varchar(Max)         null,
   Creado               datetime             not null default getdate(),
   constraint PK_LOGERROR primary key (LogErrorId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LogError') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'LogError' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Registros de errores ocurridos en el sistema.', 
   'schema', @CurrentUser, 'table', 'LogError'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LogErrorId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'LogErrorId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador del registro de error',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'LogErrorId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modulo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Modulo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Codigo del modulo en que ocurrio la excepcion',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Modulo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Entorno')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Entorno'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica el entorno de ejecución (DEV,QA,PRD)',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Entorno'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IpOrigen')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'IpOrigen'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Ip del lugar en que ocurrió el evento',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'IpOrigen'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InformacionAdicional')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InformacionAdicional'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Información adicional de un error',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InformacionAdicional'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HResult')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'HResult'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'HResult ocurrido en una excepción de sistema',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'HResult'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Message')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Message'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Mensaje de la excepción',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Message'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StackTrace')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'StackTrace'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Valor de StackTrace ocurrido en una excepcion',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'StackTrace'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Source')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Source'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Descripcion de donde se origino la excepcion',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Source'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TargetSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'TargetSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Valor de TargetSite ocurrido en una excepcion',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'TargetSite'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InnerExceptionHResult')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionHResult'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Valor de InnerExceptionHResult ocurrido en una excepción',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionHResult'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InnerExceptionMessage')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionMessage'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Valor de InnerMensajeExcepcion ocurrido en una excepcion',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionMessage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InnerExceptionSource')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionSource'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Valor de InnerOrigenException ocurrido en una excepcin',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionSource'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InnerExceptionStackTrace')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionStackTrace'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Valor de InnerExceptionStackTrace ocurrido en una excepcion',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionStackTrace'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InnerExceptionTargetSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionTargetSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Valor de InnerExceptionTargetSite ocurrido en una excepcion',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'InnerExceptionTargetSite'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogError')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'LogError', 'column', 'Creado'
go

/*==============================================================*/
/* Table: LogEvento                                             */
/*==============================================================*/
create table LogEvento (
   LogEventoId          int                  identity,
   Modulo               varchar(100)         null,
   Entorno              varchar(100)         null,
   IpOrigen             varchar(25)          null,
   TipoLog              varchar(100)         null,
   Mensaje              Varchar(Max)         null,
   Creado               datetime             not null default getdate(),
   constraint PK_LOGEVENTO primary key (LogEventoId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LogEvento') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'LogEvento' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Registros de acciones realizadas por cada usuario en el sistema.', 
   'schema', @CurrentUser, 'table', 'LogEvento'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogEvento')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LogEventoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'LogEventoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador del registro de evento',
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'LogEventoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogEvento')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modulo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'Modulo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Codigo identificador de log de eventos',
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'Modulo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogEvento')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Entorno')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'Entorno'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica el entorno de ejecución (DEV,QA,PRD)',
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'Entorno'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogEvento')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IpOrigen')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'IpOrigen'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Ip del lugar en que ocurrió el error',
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'IpOrigen'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogEvento')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TipoLog')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'TipoLog'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Tipo de Log',
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'TipoLog'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogEvento')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Mensaje')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'Mensaje'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Descripcion del registro',
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'Mensaje'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LogEvento')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'LogEvento', 'column', 'Creado'
go

/*==============================================================*/
/* Table: Notificacion                                          */
/*==============================================================*/
create table Notificacion (
   NotificacionId       int                  identity,
   UsuarioId            int                  null,
   TipoNotificacionId   int                  null,
   Mensaje              varchar(500)         not null,
   Url                  varchar(500)         null,
   Visto                bit                  not null default 0,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_NOTIFICACION primary key (NotificacionId)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Notificacion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UsuarioId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Notificacion', 'column', 'UsuarioId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de usuario',
   'schema', @CurrentUser, 'table', 'Notificacion', 'column', 'UsuarioId'
go

/*==============================================================*/
/* Table: Periodo                                               */
/*==============================================================*/
create table Periodo (
   PeriodoId            int                  identity,
   Habilitado           bit                  null,
   FechaInicio          datetime             null,
   FechaFin             datetime             null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_PERIODO primary key (PeriodoId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Periodo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'Periodo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Periodos de un Quien es Quien, periodo de Vigencia a ser mostrado, y periodo que duró la creación del Quien es Quien', 
   'schema', @CurrentUser, 'table', 'Periodo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Periodo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PeriodoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'PeriodoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador del periodo',
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'PeriodoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Periodo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Habilitado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Habilitado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identifica al periodo válido en curso, debe haber solo 1 registro habilitado',
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Habilitado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Periodo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FechaInicio')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'FechaInicio'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha inicio de un periodo',
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'FechaInicio'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Periodo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FechaFin')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'FechaFin'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha final de un periodo',
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'FechaFin'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Periodo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Periodo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Periodo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Periodo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Periodo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'Periodo', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: Puesto                                                */
/*==============================================================*/
create table Puesto (
   PuestoId             int                  identity,
   Nombre               varchar(256)         null,
   Descripcion          text                 null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_PUESTO primary key (PuestoId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Puesto') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'Puesto' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Catálogo de puestos de trabajos.', 
   'schema', @CurrentUser, 'table', 'Puesto'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Puesto')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PuestoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'PuestoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de un puesto de trabajo',
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'PuestoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Puesto')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Nombre')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Nombre'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Nombre del registro',
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Nombre'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Puesto')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Descripcion')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Descripcion'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Descripcion del registro',
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Descripcion'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Puesto')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Puesto')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Puesto')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Puesto')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Puesto')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'Puesto', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: Rol                                                   */
/*==============================================================*/
create table Rol (
   RolId                int                  identity,
   Nombre               varchar(256)         not null,
   Descripcion          text                 null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   EsSuperUsuario       bit                  null,
   constraint PK_ROL primary key (RolId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Rol') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'Rol' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Catálogo de tipo de perfiles que contienen un conjunto de permisos para asignar a un usuario.', 
   'schema', @CurrentUser, 'table', 'Rol'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Rol')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RolId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'RolId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de un rol de usuario',
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'RolId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Rol')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Nombre')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Nombre'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Nombre del registro',
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Nombre'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Rol')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Descripcion')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Descripcion'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Descripcion del registro',
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Descripcion'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Rol')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Rol')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Rol')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Rol')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Rol')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'Modificado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Rol')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EsSuperUsuario')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'EsSuperUsuario'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si el rol es para super usuario',
   'schema', @CurrentUser, 'table', 'Rol', 'column', 'EsSuperUsuario'
go

/*==============================================================*/
/* Table: RolPermiso                                            */
/*==============================================================*/
create table RolPermiso (
   RolPermisoId         int                  identity,
   ComponenteId         int                  null,
   RolId                int                  null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_ROLPERMISO primary key (RolPermisoId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('RolPermiso') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'RolPermiso' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Guarda las relaciones de los permisos a los componentes que tendrá cada rol de usuario.', 
   'schema', @CurrentUser, 'table', 'RolPermiso'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolPermiso')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RolPermisoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'RolPermisoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de relación rol y permiso',
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'RolPermisoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolPermiso')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ComponenteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'ComponenteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de componente',
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'ComponenteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolPermiso')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RolId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'RolId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de un rol de usuario',
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'RolId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolPermiso')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolPermiso')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolPermiso')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolPermiso')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolPermiso')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'RolPermiso', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: RolUsuario                                            */
/*==============================================================*/
create table RolUsuario (
   RolUsuarioId         int                  identity,
   UsuarioId            int                  null,
   RolId                int                  null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_ROLUSUARIO primary key (RolUsuarioId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('RolUsuario') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'RolUsuario' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Indica los roles que puede tener asociados un usuario.', 
   'schema', @CurrentUser, 'table', 'RolUsuario'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolUsuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RolUsuarioId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'RolUsuarioId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de rol y usuario',
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'RolUsuarioId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolUsuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UsuarioId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'UsuarioId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de Usuario',
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'UsuarioId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolUsuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RolId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'RolId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de un rol de usuario',
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'RolId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolUsuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolUsuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolUsuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolUsuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolUsuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'RolUsuario', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: TipoComponente                                        */
/*==============================================================*/
create table TipoComponente (
   TipoComponenteId     int                  identity,
   Codigo               varchar(256)         not null,
   Nombre               varchar(256)         not null,
   Descripcion          text                 null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_TIPOCOMPONENTE primary key (TipoComponenteId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('TipoComponente') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'TipoComponente' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Catálogo de tipo de componentes a utilizar en el sistema, por ejemplo, vista, vista parcial, endpoint, etc.', 
   'schema', @CurrentUser, 'table', 'TipoComponente'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoComponente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TipoComponenteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'TipoComponenteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de tipo de componente',
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'TipoComponenteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoComponente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Codigo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Codigo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Codigo distintivo del registro',
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Codigo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoComponente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Nombre')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Nombre'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Nombre del registro',
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Nombre'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoComponente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Descripcion')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Descripcion'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Descripcion del registro',
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Descripcion'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoComponente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoComponente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoComponente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoComponente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoComponente')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'TipoComponente', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: TipoDato                                              */
/*==============================================================*/
create table TipoDato (
   TipoDatoId           int                  identity,
   Codigo               varchar(50)          not null,
   Nombre               varchar(250)         not null,
   Descripcion          varchar(500)         null,
   Formato              varchar(250)         null,
   Activo               bit                  null default 1,
   Creador              int                  not null,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_TIPODATO primary key (TipoDatoId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('TipoDato') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'TipoDato' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Tipo de dato de una caracteristica de QEQ', 
   'schema', @CurrentUser, 'table', 'TipoDato'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TipoDatoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'TipoDatoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador del Tipo de Dato',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'TipoDatoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Codigo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Codigo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Codigo distintivo del registro',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Codigo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Nombre')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Nombre'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Nombre del Tipo de Dato',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Nombre'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Descripcion')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Descripcion'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Detalles sobre el tipo de dato',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Descripcion'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Formato')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Formato'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Expresiones regulares para validar el tipo de dato',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Formato'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TipoDato')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'TipoDato', 'column', 'Modificado'
go

/*==============================================================*/
/* Table: TipoNotificacion                                      */
/*==============================================================*/
create table TipoNotificacion (
   TipoNotificacionId   int                  not null,
   Codigo               varchar(25)          not null,
   Nombre               varchar(250)         not null,
   Descripcion          varchar(500)         not null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default 'datetime()',
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_TIPONOTIFICACION primary key (TipoNotificacionId)
)
go

/*==============================================================*/
/* Table: Usuario                                               */
/*==============================================================*/
create table Usuario (
   UsuarioId            int                  identity,
   EmpleadoId           int                  null,
   UserName             varchar(256)         not null,
   Password             varchar(350)         not null,
   IntentosFallidos     int                  null,
   Activo               bit                  not null default 1,
   Creador              int                  not null default 1,
   Creado               datetime             not null default getdate(),
   Modificador          int                  null,
   Modificado           datetime             null,
   constraint PK_USUARIO primary key (UsuarioId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Usuario') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'Usuario' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Catálogo de usuarios que utilizarán el sistema', 
   'schema', @CurrentUser, 'table', 'Usuario'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UsuarioId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'UsuarioId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de usuario',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'UsuarioId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EmpleadoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'EmpleadoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Identificador de un empleado',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'EmpleadoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'UserName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Nombre de usuario',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'UserName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Password')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Password'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Contraseña cifrada del usuario',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Password'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IntentosFallidos')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'IntentosFallidos'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Cantidad de intentos fallidos de cada inicio de sesion de un usuario',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'IntentosFallidos'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Activo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Activo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Indica si un registro esta activo en el sistema',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Activo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Creador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id de usuario creador del registro',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Creador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Creado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Creado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de creacion del registro',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Creado'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificador')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Modificador'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id del usuario que realizo la última modificacion del registro',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Modificador'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Usuario')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Modificado')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Modificado'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Fecha y hora de ultima modificacion del registro',
   'schema', @CurrentUser, 'table', 'Usuario', 'column', 'Modificado'
go

alter table ColumnaExcel
   add constraint FK_COLUMNAE_REFERENCE_TIPODATO foreign key (TipoDatoId)
      references TipoDato (TipoDatoId)
go

alter table Componente
   add constraint FK_Componente_Componente foreign key (PadreId)
      references Componente (ComponenteId)
go

alter table Componente
   add constraint FK_Componente_TipoComponente foreign key (TipoComponenteId)
      references TipoComponente (TipoComponenteId)
go

alter table DetallePlanilla
   add constraint FK_DETALLEP_REFERENCE_ENCABEZA foreign key (EncabezadoPlanillaId)
      references EncabezadoPlanilla (EncabezadoPlanillaId)
go

alter table Empleado
   add constraint FK_Empleado_Puesto foreign key (PuestoId)
      references Puesto (PuestoId)
go

alter table EncabezadoPlanilla
   add constraint FK_ENCABEZA_REFERENCE_PERIODO foreign key (PeriodoId)
      references Periodo (PeriodoId)
go

alter table EncabezadoPlanilla
   add constraint FK_ENCABEZA_REFERENCE_ESTADOPL foreign key (EstadoPlanillaId)
      references EstadoPlanilla (EstadoPlanillaId)
go

alter table Notificacion
   add constraint FK_NOTIFICA_REFERENCE_USUARIO foreign key (UsuarioId)
      references Usuario (UsuarioId)
go

alter table Notificacion
   add constraint FK_NOTIFICA_REFERENCE_TIPONOTI foreign key (TipoNotificacionId)
      references TipoNotificacion (TipoNotificacionId)
go

alter table RolPermiso
   add constraint FK_RolPermiso_Componente foreign key (ComponenteId)
      references Componente (ComponenteId)
go

alter table RolPermiso
   add constraint FK_RolPermiso_Rol foreign key (RolId)
      references Rol (RolId)
go

alter table RolUsuario
   add constraint FK_RolUsuario_Rol foreign key (RolId)
      references Rol (RolId)
go

alter table RolUsuario
   add constraint FK_RolUsuario_Usuario foreign key (UsuarioId)
      references Usuario (UsuarioId)
go

alter table Usuario
   add constraint FK_Usuario_Empleado foreign key (EmpleadoId)
      references Empleado (EmpleadoId)
go

