import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { appServices } from 'src/app/Services/appServices';
import { SharedService } from 'src/app/Services/sharedService';
import { HttpParams } from '@angular/common/http';
import { BoletaPagoDTO } from 'src/app/DTO/BoletaPagoDTO';
import { DetallePlanillaDTO } from 'src/app/DTO/DetallePlanillaDTO';
import { DetalleBoletaComponent } from './detalle-boleta/detalle-boleta.component';
import { ResumenBoletaPagoDTO } from 'src/app/DTO/ResumenBoletaPagoDTO';

@Component({
  selector: 'app-boleta-pago',
  templateUrl: './boleta-pago.component.html',
  styleUrls: ['./boleta-pago.component.scss']
})
export class BoletaPagoComponent implements OnInit, OnDestroy {

  ref: DynamicDialogRef;

  listaRegistros: ResumenBoletaPagoDTO[];
  textoHeaderDialogo = 'Detalle de Boleta de Pago';
  fechaInicio: Date = new Date();
  fechaFin: Date = new Date();
  listaAnios: any[] = [];
  listaMeses: any[] = [];
  anioSelected:any;
  mesSelected:any;

  constructor(private sharedService: SharedService, private service: appServices<any>, public dialogService: DialogService, private fb: FormBuilder) { }

  ngOnInit() {
    this.ObtenerAnios();
  }

  ObtenerAnios() {
    this.service.OtroGet('planilla', 'ObtenerAniosPlanilla', null).subscribe((data: number[]) => {
      if (data != null) {
        data.forEach(element => {
          this.listaAnios.push({code:element, name:element});
        });
      }
    }
    );
  }

  ObtenerMeses(event: any) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("anio", event.value.code);
    this.service.OtroGet('planilla', 'ObtenerMesesPlanilla', queryParams).subscribe((data: string[]) => {
      if (data != null) {
        this.listaMeses=data;
      }
    }
    );
  }

  mostrarDetalle(registroId) {
      this.ref = this.dialogService.open(DetalleBoletaComponent, {
          data: {
              registroId: registroId,
          },
          header: this.textoHeaderDialogo,
          width: '65%',
          height:'80%',
          styleClass: 'dynamicDialog'

      });

      this.ref.onClose.subscribe((resultadoExitoso: boolean) => {
          if (resultadoExitoso) {
              this.ObtenerRegistros();
          }
      });
  }

  ObtenerRegistros() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("anio", this.anioSelected.code);
    queryParams = queryParams.append("mes", this.mesSelected.code);
    queryParams = queryParams.append("userId", localStorage.getItem('userId'));

      this.service.OtroGet('planilla', 'ObtenerResumenBoletaEmpleado', queryParams).subscribe((data: ResumenBoletaPagoDTO[]) => {
        if(data!=null)
          this.listaRegistros = data;
      }
      );
  }

  ngOnDestroy() {
    if (this.ref) {
      this.ref.close();
    }
  }

}
