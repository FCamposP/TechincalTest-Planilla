import { HttpParams } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EncabezadoPlanillaDTO } from 'src/app/DTO/EncabezadoPlanillaDTO';
import { appServices } from 'src/app/Services/appServices';
import { SharedService } from 'src/app/Services/sharedService';
import Swal from 'sweetalert2';
import { TablaPagosComponent } from './tabla-pagos/tabla-pagos.component';
import { MenuItem } from 'primeng/api';
import { saveAs } from 'file-saver';
import { CargarPlanillaComponent } from './cargar-planilla/cargar-planilla.component';

@Component({
  selector: 'app-planilla-pagos',
  templateUrl: './planilla-pagos.component.html',
  styleUrls: ['./planilla-pagos.component.scss']
})
export class PlanillaPagosComponent implements OnInit, OnDestroy {

  ref: DynamicDialogRef;

  listaRegistros: EncabezadoPlanillaDTO[];
  esNuevo: boolean = true;
  textoHeaderDialogo = '';
  registrosSeleccionados: EncabezadoPlanillaDTO[];


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
      this.crear = this.sharedService.buscarPermisoAcciones("planilla", "Crear");
      this.editar = this.sharedService.buscarPermisoAcciones("planilla", "Editar");
      this.eliminar = this.sharedService.buscarPermisoAcciones("planilla", "Eliminar");
  }

  crearNuevo() {
      this.esNuevo = true;
      this.textoHeaderDialogo = "Nueva planilla";
      this.mostrarDialogo(-1);
  }

  editarRegistro(registroId) {
      this.esNuevo = false;
      this.textoHeaderDialogo = "Detalle de planilla"
      this.mostrarDialogo(registroId);
  }

  mostrarDialogo(registroId) {
      this.ref = this.dialogService.open(CargarPlanillaComponent, {
          data: {
              registroId: registroId,
              esNuevo: this.esNuevo
          },
          header: this.textoHeaderDialogo,
          width: '85%',
          height: '80%',
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
      this.service.OtroGet('planilla', '', null).subscribe((data: EncabezadoPlanillaDTO[]) => {
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

              this.service.OtroDelete('planilla', '', queryParams).subscribe((data: EncabezadoPlanillaDTO) => {
                  if (data != null) {
                      Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro eliminado con éxito', showConfirmButton: false, timer: 3500, toast: true });
                      this.ObtenerRegistros();
                  }
              }
              );
          }
      })
  }

  
  deshabilitarPlanilla(registroId) {
    let planilla=this.listaRegistros.find(x=>x.encabezadoPlanillaId==registroId);

    Swal.fire({
        title: planilla.habilitado==true?'Deshabilitar':"Habilitar",
        text: planilla.habilitado==true?"¿Realmente desea deshabilitar la planilla?. Ya no será visible a los empleados": "¿Realmente desea habilitar la planilla?",
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

            this.service.OtroPost('planilla', 'DeshabilitarPlanilla',null, queryParams).subscribe((data: EncabezadoPlanillaDTO) => {
                if (data != null) {
                    Swal.fire({ position: 'top-end', icon: 'success', text: 'Planilla actualizada con éxito', showConfirmButton: false, timer: 3500, toast: true });
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
                      listaIds.push(element.encabezadoPlanillaId);
                  });
                  this.service.OtroPost('planilla', 'EliminarMultiples', listaIds).subscribe((data: EncabezadoPlanillaDTO) => {
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

  descargarPlantilla() {
    this.service.OtroGet('planilla', 'descargarPlantilla').subscribe((data: any) => {
      if (data != null) {
        this.downloadPDF(data)
      }
    }
    );
  }

  downloadPDF(plantilla) {
    const linkSource = `data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,${plantilla.archivo}`;
    const downloadLink = document.createElement("a");

    downloadLink.href = linkSource;
    downloadLink.download = plantilla.nombre;
    downloadLink.click();
}

  ngOnDestroy() {
      if (this.ref) {
          this.ref.close();
      }
  }

}
