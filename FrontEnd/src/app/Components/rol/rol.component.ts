import { Component, OnDestroy, OnInit } from "@angular/core";
import { appServices } from "../../Services/appServices";
import { ConfirmationService } from "primeng/api";
import { MessageService } from "primeng/api";
import Swal from "sweetalert2/dist/sweetalert2.js";
import { DialogService, DynamicDialogRef } from "primeng/dynamicdialog";
import { RolDTO } from "src/app/DTO/RolDTO";
import { HttpParams } from "@angular/common/http";
import { RolFormComponent } from "./rol-form/rol-form.component";
import { SharedService } from "../../Services/sharedService";

@Component({
    templateUrl: "./rol.component.html",

    // styleUrls: ["./../../../assets/demo/badges.scss"],

    providers: [MessageService, ConfirmationService, appServices],
})
export class RolComponent implements OnInit, OnDestroy {

    ref: DynamicDialogRef;

    listaRegistros: RolDTO[];
    esNuevo: boolean = true;
    textoHeaderDialogo = '';
    registrosSeleccionados: RolDTO[];

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
        this.crear = this.sharedService.buscarPermisoAcciones("rol", "Crear");
        this.editar = this.sharedService.buscarPermisoAcciones("rol", "Editar");
        this.eliminar = this.sharedService.buscarPermisoAcciones("rol", "Eliminar");
    }

    crearNuevo() {
        this.esNuevo = true;
        this.textoHeaderDialogo = "Nuevo Rol";
        this.mostrarDialogo(-1);
    }

    editarRegistro(registroId) {
        this.esNuevo = false;
        this.textoHeaderDialogo = "Detalle de Rol"
        this.mostrarDialogo(registroId);
    }

    mostrarDialogo(registroId) {
        this.ref = this.dialogService.open(RolFormComponent, {
            data: {
                registroId: registroId,
                esNuevo: this.esNuevo
            },
            header: this.textoHeaderDialogo,
            width: '70%',
            height: '80%',
            styleClass: 'dynamicDialog'
        });

        this.ref.onClose.subscribe((resultadoExitoso: boolean) => {
            if (resultadoExitoso) {
                this.ObtenerRegistros();
            }
        });
    }

    ObtenerRegistros() {
        this.service.OtroGet('rol', '', null).subscribe((data: RolDTO[]) => {
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

                this.service.OtroDelete('rol', '', queryParams).subscribe((data: RolDTO) => {
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
                        listaIds.push(element.rolId);
                    });
                    this.service.OtroPost('rol', 'EliminarMultiples', listaIds).subscribe((data: RolDTO) => {
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
