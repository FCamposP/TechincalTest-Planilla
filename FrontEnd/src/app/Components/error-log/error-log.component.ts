import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ErrorLogDTO } from 'src/app/DTO/ErrorLogDTO';
import { appServices } from 'src/app/Services/appServices';
import { SharedService } from 'src/app/Services/sharedService';
import { ErrorFormComponent } from './error-form/error-form.component';
import Swal from 'sweetalert2';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PeriodoDTO } from 'src/app/DTO/PeriodoDTO';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-error-log',
  templateUrl: './error-log.component.html',
  styleUrls: ['./error-log.component.scss']
})
export class ErrorLogComponent implements OnInit, OnDestroy {

  ref: DynamicDialogRef;

  listaRegistros: ErrorLogDTO[];
  textoHeaderDialogo = 'Detalle de Error';
  registrosSeleccionados: ErrorLogDTO[];
  registro: PeriodoDTO;
  fechaInicio: Date= new Date();
  fechaFin: Date= new Date();

  constructor(private sharedService: SharedService,private service: appServices<any>, public dialogService: DialogService, private fb: FormBuilder) { }

  ngOnInit() {
      this.registrosSeleccionados = [];
      this.ObtenerRegistros();

  }

  changeFechaInicio() {
    if(this.fechaInicio>this.fechaFin){
      this.fechaFin=this.fechaInicio
    }
  }

  changeFechaFin() {
    if(this.fechaFin<this.fechaInicio){
      this.fechaInicio=this.fechaFin
    }
  }

  mostrarDetalle(registroId) {
      this.ref = this.dialogService.open(ErrorFormComponent, {
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
              // this.ObtenerRegistros();
          }
      });
  }

  ObtenerRegistros() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("fechaInicio", this.fechaInicio.toISOString());
    queryParams = queryParams.append("fechaFin", this.fechaFin.toISOString());
      this.service.OtroGet('errorlog', 'ObtenerErroresPorFechas', queryParams).subscribe((data: ErrorLogDTO[]) => {
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
