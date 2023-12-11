import { Injectable } from "@angular/core";
import { SeguridadPermisoDTO } from "../DTO/SeguridadPermisoDTO";

@Injectable({ providedIn: 'root' })
export class SharedService {
    //Permisos que va a tener el usuario sobre las vistas y opciones
    private permisosOpciones!: string;

    constructor() {

    }

    set PermisosOpciones(valor: string) {
        localStorage.setItem('permisos', valor);
        this.permisosOpciones = valor;
    }
    get PermisosOpciones() {
        if (this.permisosOpciones) {
            return this.permisosOpciones;
        } else {
            return (localStorage.getItem('permisos') || '');
        }
    }

    buscarPermiso(url: string): boolean {
        let tienePermiso: boolean = false;
        let permisos: SeguridadPermisoDTO[] = JSON.parse(this.PermisosOpciones);

        if (permisos && permisos != undefined && permisos.length > 0) {
            let permisoEncontrado = permisos.find(x => x.superUsuario);
            if (permisoEncontrado) {
                return true;
            }
            permisoEncontrado = permisos.find(x => x.url?.trim().toUpperCase() === url.trim().toUpperCase());
            if (permisoEncontrado) {
                tienePermiso = true;
            }
        }
        return tienePermiso;
    }

    buscarPermisoAcciones(padre: string, opcion: string): boolean {
        let tienePermiso: boolean = false;
        let permisos: SeguridadPermisoDTO[] = JSON.parse(this.PermisosOpciones);

        if (permisos && permisos != undefined && permisos.length > 0) {
            let permisoEncontrado = permisos.find(x => x.superUsuario);
            if (permisoEncontrado) {
                return true;
            }
            permisoEncontrado = permisos.find(x => x.padre?.trim()?.toUpperCase() === padre.trim()?.toUpperCase() && x.nombreComponente.trim()?.toUpperCase() === opcion.trim().toUpperCase());
            if (permisoEncontrado) {
                tienePermiso = true;
            }
        }
        return tienePermiso;
    }

}