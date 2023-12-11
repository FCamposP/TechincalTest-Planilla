import { Component, OnDestroy, OnInit } from "@angular/core";
import { appServices } from "../../Services/appServices";
import { ConfirmationService } from "primeng/api";
import { MessageService } from "primeng/api";
import Swal from "sweetalert2/dist/sweetalert2.js";
import { HttpParams } from "@angular/common/http";
import { DynamicDialogRef, DialogService } from "primeng/dynamicdialog";
import { ConfiguracionGlobalDTO } from "src/app/DTO/ConfiguracionGlobalDTO";
import { ConfiguracionGlobalFormComponent } from "./configuracion-global-form/configuracion-global-form.component";

@Component({
  selector: 'app-configuracion-global',
  templateUrl: './configuracion-global.component.html',
  styleUrls: ['./configuracion-global.component.scss'],
  providers: [MessageService, ConfirmationService]
})

export class ConfiguracionGlobalComponent implements OnInit, OnDestroy {

  ref: DynamicDialogRef;

    listaRegistros: ConfiguracionGlobalDTO[];
    esNuevo: boolean = true;
    textoHeaderDialogo = '';
    registrosSeleccionados: ConfiguracionGlobalDTO[];

    constructor(private service: appServices<any>, public dialogService: DialogService) { }

    ngOnInit() {
        this.registrosSeleccionados = [];
        this.ObtenerRegistros();
    }

    crearNuevo() {
        this.esNuevo = true;
        this.textoHeaderDialogo = "Nueva configuración global";
        this.mostrarDialogo(-1);
    }

    editarRegistro(registroId) {
        this.esNuevo = false;
        this.textoHeaderDialogo = "Detalle de configuración global"
        this.mostrarDialogo(registroId);
    }

    mostrarDialogo(registroId) {
        this.ref = this.dialogService.open(ConfiguracionGlobalFormComponent, {
            data: {
                registroId: registroId,
                esNuevo: this.esNuevo
            },
            header: this.textoHeaderDialogo,
            width: '60%',
            height:'75%',
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
        this.service.OtroGet('configuracionglobal', '', null).subscribe((data: ConfiguracionGlobalDTO[]) => {
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

                this.service.OtroDelete('configuracionglobal', '', queryParams).subscribe((data: ConfiguracionGlobalDTO) => {
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
                        listaIds.push(element.configuracionId);
                    });
                    this.service.OtroPost('configuracionglobal', 'EliminarMultiples', listaIds).subscribe((data: ConfiguracionGlobalDTO) => {
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
