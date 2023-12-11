export class SeguridadPermisoDTO {
    rolPermisoId?: number;

    componenteId?: number;
    rolId?: number;
    activo?: boolean;
    nombreComponente: string;
    descripcion?: string;
    url: string;
    superUsuario: boolean;
    padre: string;
    constructor() {
        this.descripcion = '';
        this.superUsuario = false;

    }
}