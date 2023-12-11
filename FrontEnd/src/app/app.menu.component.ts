import { Component, OnInit } from '@angular/core';
import { appServices } from './Services/appServices';
import { ComponenteDTO } from './DTO/ComponenteDTO';
import { ComponenteNavegacion } from './DTO/ComponenteNavegacion';
import { HttpParams } from '@angular/common/http';

@Component({
    selector: 'app-menu',
    template: `
    <ul class="layout-menu layout-main-menu clearfix">
        <li app-menuitem *ngFor="let item of model; let i = index;" [item]="item" [index]="i" [root]="true"></li>
    </ul>
    `
})
export class AppMenuComponent implements OnInit {

    model: any[];
    constructor(private service: appServices<any>) { }


    ngOnInit() {
        this.model=[];
        this.obtenerNavegacion();
        //this.ejemploNavegacion();
    }

    obtenerNavegacion(){
        let user = localStorage.getItem("userId");        
        let queryParams = new HttpParams();
        queryParams = queryParams.append("userId", user);
        this.service.OtroGet('componente', 'ObtenerNavegacion', queryParams).subscribe((data: ComponenteNavegacion[]) => {
            var nuevoMenu= new ComponenteNavegacion();
            nuevoMenu.label='Catalogos';
            nuevoMenu.items= data;
            this.model.push(nuevoMenu);
        }
        );
    }

    ejemploNavegacion(){
        this.model = [
            {
                label: 'Catalogos',
                items: [
                    {
                        label: 'Mantenimientos', icon: 'pi pi-fw pi-cog',
                        items: [
                            {label: 'Caracteristicas', icon: 'pi pi-fw pi-map-marker', routerLink: ['/mantenimiento/caracteristica'] },
                            {label: 'Configuración Global', icon: 'pi pi-fw pi-info', routerLink: ['/mantenimiento/configuracion-global'] },
                            {label: 'Establecimientos', icon: 'pi pi-fw pi-map-marker', routerLink: ['/mantenimiento/establecimiento'] },
                            {label: 'Empleados', icon: 'pi pi-fw pi-user', routerLink: ['/mantenimiento/empleados'] },
                            {label: 'Empresas', icon: 'pi pi-fw pi-clone', routerLink: ['/mantenimiento/empresa'] },
                            {label: 'Periodos', icon: 'pi pi-fw pi-calendar', routerLink: ['/mantenimiento/periodo'] },
                            {label: 'Puestos', icon: 'pi pi-fw pi-calendar', routerLink: ['/mantenimiento/puesto'] },
                            
                        ],
                    },
                    {
                        label: 'Datos Geográficos', icon: 'pi pi-fw pi-globe',
                        items: [
                            { label: 'Departamentos', icon: 'pi pi-fw pi-map', routerLink: ['/mantenimiento/departamento'] },
                            { label: 'Establecimientos', icon: 'pi pi-fw pi-map-marker', routerLink: ['/mantenimiento/establecimiento'] },
                            { label: 'Municipios', icon: 'pi pi-fw pi-map', routerLink: ['/mantenimiento/municipio'] },
                            { label: 'Regiones', icon: 'pi pi-fw pi-map', routerLink: ['/mantenimiento/region'] },
                        ]
                    },
                    {
                        label: 'Usuarios y Permisos', icon: 'pi pi-fw pi-users',
                        items: [
                            { label: 'Componentes', icon: '', routerLink: ['/mantenimiento/componentes'] },
                            { label: 'Roles', icon: '', routerLink: ['/mantenimiento/rol'] },
                            { label: 'Usuarios', icon: '', routerLink: ['/mantenimiento/usuario'] },
                        ]
                    },
                    {
                        label: 'Quién es Quién', icon: 'pi pi-fw pi-question',
                        items: [
                            // { label: 'Importar QeQ', icon: 'pi pi-fw pi-file', routerLink: ['/mantenimiento/importQeQ'] },
                            { label: 'Argumentos', icon: 'pi pi-fw pi-map-marker', routerLink: ['/mantenimiento/argumento'] },
                            { label: 'Etapas', icon: 'pi pi-fw pi-clone', routerLink: ['/mantenimiento/etapa'] },
                            { label: 'Listado de QeQ', icon: 'pi pi-fw pi-list', routerLink: ['/mantenimiento/qeq'] },
                            { label: 'Plantillas', icon: 'pi pi-fw pi-list', routerLink: ['/mantenimiento/configuracionQeQ'] },
                            { label: 'Productos', icon: 'pi pi-fw pi-home', routerLink: ['/mantenimiento/producto'] },
                            { label: 'Tipo de Argumentos', icon: 'pi pi-fw pi-file', routerLink: ['/mantenimiento/tipo-argumento'] },

                        ]
                    },
                    {
                        label: 'Gestión de Tipos', icon: 'pi pi-fw pi-box',
                        items: [
                            { label: 'Tipo de Argumentos', icon: 'pi pi-fw pi-file', routerLink: ['/mantenimiento/tipo-argumento'] },
                            { label: 'Tipo de Componentes', icon: 'pi pi-fw pi-file', routerLink: ['/mantenimiento/tipo-componente'] },
                            { label: 'Tipo de Datos', icon: 'pi pi-fw pi-file', routerLink: ['/mantenimiento/tipo-dato'] },
                            { label: 'Tipo de Formatos', icon: 'pi pi-fw pi-file', routerLink: ['/mantenimiento/tipo-formato'] },
                            //{ label: 'Tipo de notificación', icon: 'pi pi-fw pi-file', routerLink: ['/mantenimiento/tipoNotificacion'] },
                            { label: 'Tipo de Periodos', icon: 'pi pi-fw pi-file', routerLink: ['/mantenimiento/tipo-periodo'] }
                        ]
                    },
                ]
            }
        ];
    }
}
