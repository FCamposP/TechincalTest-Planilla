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

  itemsDescarga: MenuItem[];


  constructor(private sharedService: SharedService, private service: appServices<any>, public dialogService: DialogService) { }

  ngOnInit() {
      this.registrosSeleccionados = [];
      this.ObtenerRegistros();
      this.buscarPermisos();

      this.itemsDescarga = [
        {
          label: 'Descargar PDF',
          icon: 'pi pi-download',
          command: () => {
            // this.descargarArchivo('pdf');
          }
        },
      ];
  }

  crear: boolean = false;
  editar: boolean = false;
  eliminar: boolean = false;

  buscarPermisos() {
      this.crear = this.sharedService.buscarPermisoAcciones("planillapagos", "Crear");
      this.editar = this.sharedService.buscarPermisoAcciones("planillapagos", "Editar");
      this.eliminar = this.sharedService.buscarPermisoAcciones("planillapagos", "Eliminar");
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
      this.ref = this.dialogService.open(TablaPagosComponent, {
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
          if (resultadoExitoso) {
              this.ObtenerRegistros();
              // this.messageService.add({severity:'info', summary: 'Car Selected', detail:'Vin:' + car.vin});
          }
      });
  }

  ObtenerRegistros() {
      this.service.OtroGet('planillapagos', '', null).subscribe((data: EncabezadoPlanillaDTO[]) => {
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

              this.service.OtroDelete('planillapagos', '', queryParams).subscribe((data: EncabezadoPlanillaDTO) => {
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
                      listaIds.push(element.encabezadoPlanillaId);
                  });
                  this.service.OtroPost('puesto', 'EliminarMultiples', listaIds).subscribe((data: EncabezadoPlanillaDTO) => {
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
