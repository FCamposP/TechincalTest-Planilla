import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { BoletaPagoDTO } from 'src/app/DTO/BoletaPagoDTO';
import { appServices } from 'src/app/Services/appServices';

@Component({
  selector: 'app-detalle-boleta',
  templateUrl: './detalle-boleta.component.html',
  styleUrls: ['./detalle-boleta.component.scss']
})
export class DetalleBoletaComponent implements OnInit {

  registroId: number = -1;
  detalleBoletaPago: BoletaPagoDTO;
  
  constructor(private service: appServices<any>, public config: DynamicDialogConfig) { }
  
  ngOnInit() {
    this.registroId = this.config.data.registroId;
    this.obtenerDetalleBoletaPago();
  }

  obtenerDetalleBoletaPago(){
    let queryParams = new HttpParams();
    queryParams = queryParams.append("detallePlanillaId", this.registroId);

    this.service.OtroGet('planilla', 'ObtenerDetalleBoletaPago', queryParams).subscribe((data: BoletaPagoDTO) => {
      if(data!=null)
        this.detalleBoletaPago = data;
    }
    );
  }
}
