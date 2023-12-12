import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { LogErrorDTO } from 'src/app/DTO/LogErrorDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-error-form',
  templateUrl: './error-form.component.html',
  styleUrls: ['./error-form.component.scss']
})
export class ErrorFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig) { }

  registro: LogErrorDTO;
  submitted: boolean;
  registrosLista: LogErrorDTO[];

  registroId: number = -1

  ngOnInit() {
    this.registro = new LogErrorDTO();
    this.registroId = this.config.data.registroId;
    this.obtenerDetalleRegistro();
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("LogErrorId", this.registroId);
    this.service.OtroGet('logerror', 'ObtenerDetalleError', queryParams).subscribe((data: LogErrorDTO) => {
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
