import { Component, Input, OnInit } from '@angular/core';
import { DetallePlanillaDTO } from 'src/app/DTO/DetallePlanillaDTO';

@Component({
  selector: 'app-tabla-pagos',
  templateUrl: './tabla-pagos.component.html',
  styleUrls: ['./tabla-pagos.component.scss']
})
export class TablaPagosComponent implements OnInit {

  @Input() detallePagos: DetallePlanillaDTO[] = [];
  @Input() deshabilitarTabla: boolean=true;

  constructor() { }

  ngOnInit() {
  }

  actualizarDetalle(registro:DetallePlanillaDTO){
    if(registro!=null){
      registro.actualizar=true;
    }
  }

}
