import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ComponenteDTO } from 'src/app/DTO/ComponenteDTO';
import { appServices } from 'src/app/Services/appServices';
import { ComponenteFormComponent } from './componente-form/componente-form.component';
import Swal from 'sweetalert2';
import { HttpParams } from '@angular/common/http';
import { SelectItem } from 'primeng/api';
import { SharedService } from "../../Services/sharedService";


@Component({
    selector: 'app-componentes',
    templateUrl: './componentes.component.html',
    styleUrls: ['./componentes.component.scss']
})
export class ComponentesComponent implements OnInit, OnDestroy {

    ref: DynamicDialogRef;

    listaRegistros: ComponenteDTO[];
    esNuevo: boolean = true;
    textoHeaderDialogo = '';
    registrosSeleccionados: ComponenteDTO[];
    tipoComponentes: SelectItem[];

    constructor(private sharedService: SharedService, private service: appServices<any>, public dialogService: DialogService) { }

    ngOnInit() {
        this.registrosSeleccionados = [];
        this.ObtenerRegistros();
        this.tipoComponentes = [
            { label: 'Front Office', value: true },
            { label: 'Back Office', value: false }
        ];

        this.buscarPermisos();
    }


    crear: boolean = false;
    editar: boolean = false;
    eliminar: boolean = false;

    buscarPermisos() {
        this.crear = this.sharedService.buscarPermisoAcciones("componentes", "Crear");
        this.editar = this.sharedService.buscarPermisoAcciones("componentes", "Editar");
        this.eliminar = this.sharedService.buscarPermisoAcciones("componentes", "Eliminar");
    }

    crearNuevo() {
        this.esNuevo = true;
        this.textoHeaderDialogo = "Nuevo Componente";
        this.mostrarDialogo(-1);
    }

    editarRegistro(registroId) {
        this.esNuevo = false;
        this.textoHeaderDialogo = "Detalle de Componente"
        this.mostrarDialogo(registroId);
    }

    mostrarDialogo(registroId) {
        this.ref = this.dialogService.open(ComponenteFormComponent, {
            data: {
                registroId: registroId,
                esNuevo: this.esNuevo
            },
            header: this.textoHeaderDialogo,
            width: '70%',
            height: '70%',
            styleClass: 'dynamicDialog'
        });

        this.ref.onClose.subscribe((resultadoExitoso: boolean) => {
            if (resultadoExitoso) {
                this.ObtenerRegistros();
                // this.messageService.add({severity:'info', summary: 'Car Selected', detail:'Vin:' + car.vin});
            }
        });
    }

    ObtenerRegistros() {
        let queryParams = new HttpParams();
        this.service.OtroGet('componente', '', null).subscribe((data: ComponenteDTO[]) => {
            this.listaRegistros = data;
        }
        );
    }

    cambioComponentes() {
        this.ObtenerRegistros();
    }


    eliminarRegistro(registroId) {
        Swal.fire({
            title: 'Eliminar',
            text: "¿Realmente desea eliminar el registro?",
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Aceptar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                let queryParams = new HttpParams();
                queryParams = queryParams.append("id", registroId);
                this.service.OtroDelete('componente', '', queryParams).subscribe((data: ComponenteDTO) => {
                    if (data != null) {
                        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro eliminado con éxito', showConfirmButton: false, timer: 3500, toast: true });
                        this.ObtenerRegistros();
                    }
                }
                );
            }
        })
    }

    eliminarMultiplesRegistros() {
        if (this.registrosSeleccionados.length > 0) {
            Swal.fire({
                title: 'Eliminar',
                text: "¿Realmente desea eliminar los " + this.registrosSeleccionados.length + " registros seleccionados?",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Aceptar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    var listaIds: number[] = []
                    this.registrosSeleccionados.forEach(element => {
                        listaIds.push(element.componenteId);
                    });
                    this.service.OtroPost('componente', 'EliminarMultiples', listaIds).subscribe((data: ComponenteDTO) => {
                        if (data != null) {
                            Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro eliminado con éxito', showConfirmButton: false, timer: 3500, toast: true });
                            this.ObtenerRegistros();
                        }
                    }
                    );
                }
            })
        }
    }

    ngOnDestroy() {
        if (this.ref) {
            this.ref.close();
        }
    }

}
