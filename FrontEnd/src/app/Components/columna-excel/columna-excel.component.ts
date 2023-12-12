import { Component, OnDestroy, OnInit } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { DynamicDialogRef, DynamicDialogConfig, DialogService } from 'primeng/dynamicdialog';
import { PuestoDTO } from 'src/app/DTO/PuestoDTO';
import { ComponenteDTO } from 'src/app/DTO/ComponenteDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ColumnaExcelDTO } from 'src/app/DTO/ColumnaExcelDTO';
import { SharedService } from 'src/app/Services/sharedService';
import { ColumnaExcelFormComponent } from './columna-excel-form/columna-excel-form.component';
@Component({
  selector: 'app-columna-excel',
  templateUrl: './columna-excel.component.html',
  styleUrls: ['./columna-excel.component.scss']
})
export class ColumnaExcelComponent implements OnInit, OnDestroy {

  ref: DynamicDialogRef;

  listaRegistros: ColumnaExcelDTO[];
  esNuevo: boolean = true;
  textoHeaderDialogo = '';
  registrosSeleccionados: ColumnaExcelDTO[];

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
      this.crear = this.sharedService.buscarPermisoAcciones("columnaExcel", "Crear");
      this.editar = this.sharedService.buscarPermisoAcciones("columnaExcel", "Editar");
      this.eliminar = this.sharedService.buscarPermisoAcciones("columnaExcel", "Eliminar");
  }

  crearNuevo() {
      this.esNuevo = true;
      this.textoHeaderDialogo = "Nueva columna";
      this.mostrarDialogo(-1);
  }

  editarRegistro(registroId) {
      this.esNuevo = false;
      this.textoHeaderDialogo = "Detalle de columna"
      this.mostrarDialogo(registroId);
  }

  mostrarDialogo(registroId) {
      this.ref = this.dialogService.open(ColumnaExcelFormComponent, {
          data: {
              registroId: registroId,
              esNuevo: this.esNuevo
          },
          header: this.textoHeaderDialogo,
          width: '60%',
          height: '85%',
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
      this.service.OtroGet('columnaExcel', '', null).subscribe((data: ColumnaExcelDTO[]) => {
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

              this.service.OtroDelete('columnaExcel', '', queryParams).subscribe((data: ColumnaExcelDTO) => {
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
                      listaIds.push(element.columnaExcelId);
                  });
                  this.service.OtroPost('columnaExcel', 'EliminarMultiples', listaIds).subscribe((data: ColumnaExcelDTO) => {
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
