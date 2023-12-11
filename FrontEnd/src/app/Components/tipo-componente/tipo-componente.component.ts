import { Component, OnDestroy, OnInit } from "@angular/core";
import { appServices } from "../../Services/appServices";
import { ConfirmationService, SelectItem } from "primeng/api";
import { MessageService } from "primeng/api";
import Swal from "sweetalert2/dist/sweetalert2.js";
import { HttpParams } from "@angular/common/http";
import { DynamicDialogRef, DialogService } from "primeng/dynamicdialog";
import { TipoComponenteDTO } from "src/app/DTO/TipoComponenteDTO";
import { TipoComponenteFormComponent } from "./tipo-componente-form/tipo-componente-form.component";
import { SharedService } from "../../Services/sharedService";

@Component({
  selector: 'app-tipo-componente',
  templateUrl: './tipo-componente.component.html',
  styleUrls: ['./tipo-componente.component.scss'],
  providers: [MessageService, ConfirmationService]
})

export class TipoComponenteComponent implements OnInit, OnDestroy {

  ref: DynamicDialogRef;

  listaRegistros: TipoComponenteDTO[];
  esNuevo: boolean = true;
  textoHeaderDialogo = '';
  registrosSeleccionados: TipoComponenteDTO[];

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
    this.crear = this.sharedService.buscarPermisoAcciones("tipo-componente", "Crear");
    this.editar = this.sharedService.buscarPermisoAcciones("tipo-componente", "Editar");
    this.eliminar = this.sharedService.buscarPermisoAcciones("tipo-componente", "Eliminar");
  }


  crearNuevo() {
    this.esNuevo = true;
    this.textoHeaderDialogo = "Nuevo tipo de componente";
    this.mostrarDialogo(-1);
  }

  editarRegistro(registroId) {
    this.esNuevo = false;
    this.textoHeaderDialogo = "Detalle de tipo de componente"
    this.mostrarDialogo(registroId);
  }

  mostrarDialogo(registroId) {
    this.ref = this.dialogService.open(TipoComponenteFormComponent, {
      data: {
        registroId: registroId,
        esNuevo: this.esNuevo
      },
      header: this.textoHeaderDialogo,
      width: '50%',
      height: '60%',
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
    this.service.OtroGet('tipocomponente', '', null).subscribe((data: TipoComponenteDTO[]) => {
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

        this.service.OtroDelete('tipocomponente', '', queryParams).subscribe((data: TipoComponenteDTO) => {
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
            listaIds.push(element.tipoComponenteId);
          });
          this.service.OtroPost('tipocomponente', 'EliminarMultiples', listaIds).subscribe((data: TipoComponenteDTO) => {
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
