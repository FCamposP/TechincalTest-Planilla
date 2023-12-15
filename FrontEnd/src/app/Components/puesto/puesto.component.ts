import { Component, OnDestroy, OnInit } from "@angular/core";
import { appServices } from "../../Services/appServices";
import { ConfirmationService, SelectItem } from "primeng/api";
import { MessageService } from "primeng/api";
import Swal from "sweetalert2/dist/sweetalert2.js";
import { HttpParams } from "@angular/common/http";
import { DynamicDialogRef, DialogService } from "primeng/dynamicdialog";
import { PuestoDTO } from "src/app/DTO/PuestoDTO";
import { PuestoFormComponent } from "./puesto-form/puesto-form.component";
import { SharedService } from "../../Services/sharedService";

@Component({
    templateUrl: './puesto.component.html',
    //styleUrls: ['./../../../assets/demo/badges.scss'],
    styleUrls: ['./puesto.component.scss'],
    providers: [MessageService, ConfirmationService]
})

export class PuestoComponent implements OnInit, OnDestroy {

    ref: DynamicDialogRef;

    listaRegistros: PuestoDTO[];
    esNuevo: boolean = true;
    textoHeaderDialogo = '';
    registrosSeleccionados: PuestoDTO[];

    constructor(private sharedService: SharedService, private service: appServices<any>, public dialogService: DialogService) { }

    ngOnInit() {
        this.registrosSeleccionados = [];
        this.ObtenerRegistros();
        this.buscarPermisos();
    }

    crear: boolean = false;
    editar: boolean = false;
    eliminar: boolean = false;

    buscarPermisos() {
        this.crear = this.sharedService.buscarPermisoAcciones("puesto", "Crear");
        this.editar = this.sharedService.buscarPermisoAcciones("puesto", "Editar");
        this.eliminar = this.sharedService.buscarPermisoAcciones("puesto", "Eliminar");
    }

    crearNuevo() {
        this.esNuevo = true;
        this.textoHeaderDialogo = "Nuevo puesto";
        this.mostrarDialogo(-1);
    }

    editarRegistro(registroId) {
        this.esNuevo = false;
        this.textoHeaderDialogo = "Detalle de puesto"
        this.mostrarDialogo(registroId);
    }

    mostrarDialogo(registroId) {
        this.ref = this.dialogService.open(PuestoFormComponent, {
            data: {
                registroId: registroId,
                esNuevo: this.esNuevo
            },
            header: this.textoHeaderDialogo,
            width: '60%',
            height: '50%',
            styleClass: 'dynamicDialog'
        });

        this.ref.onClose.subscribe((resultadoExitoso: boolean) => {
            if (resultadoExitoso==true) {
                this.ObtenerRegistros();
                // this.messageService.add({severity:'info', summary: 'Car Selected', detail:'Vin:' + car.vin});
            }
        });
    }

    ObtenerRegistros() {
        this.service.OtroGet('puesto', '', null).subscribe((data: PuestoDTO[]) => {
            this.listaRegistros = data;
        }
        );
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

                this.service.OtroDelete('puesto', '', queryParams).subscribe((data: PuestoDTO) => {
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
                        listaIds.push(element.puestoId);
                    });
                    this.service.OtroPost('puesto', 'EliminarMultiples', listaIds).subscribe((data: PuestoDTO) => {
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
