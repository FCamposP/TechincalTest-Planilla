export class RolDTO {
    rolId: number;
    nombre: string;
    descripcion?: string;
    activo?: boolean;
    esSuperUsuario?:boolean;
    rolUsuarios?: RolUsuarioDTO[];
    rolPermisos?: RolPermisoDTO[];
}

export class RolUsuarioDTO {
    rolUsuarioId: number;
    usuarioId: number;
    rolId: number;
    activo: boolean;
}

export class RolPermisoDTO {
    rolPermisoId: number;
    componenteId: number;
    rolId: number;
    activo: boolean;
}

export class RolTree{
    key:string;
    label:string;
    data:string;
    icon:string;
    children:RolTree[]
}