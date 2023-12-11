import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ErrorLogDTO } from 'src/app/DTO/ErrorLogDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-error-form',
  templateUrl: './error-form.component.html',
  styleUrls: ['./error-form.component.scss']
})
export class ErrorFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig) { }

  registro: ErrorLogDTO;
  submitted: boolean;
  registrosLista: ErrorLogDTO[];

  registroId: number = -1

  ngOnInit() {
    this.registro = new ErrorLogDTO();
    this.registroId = this.config.data.registroId;
    this.obtenerDetalleRegistro();
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("errorLogId", this.registroId);
    this.service.OtroGet('errorLog', 'ObtenerDetalleError', queryParams).subscribe((data: ErrorLogDTO) => {
      if (data != null) {
        this.registro = data;
      }
    }
    );
  }

  cerrarVentana() {
    this.ref.close();
  }

}
