import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ComponenteDTO } from 'src/app/DTO/ComponenteDTO';
import { TipoComponenteDTO } from 'src/app/DTO/TipoComponenteDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-componente-form',
  templateUrl: './componente-form.component.html',
  styleUrls: ['./componente-form.component.scss']
})
export class ComponenteFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig) { }

  registro: ComponenteDTO;
  submitted: boolean;
  registrosLista: ComponenteDTO[];

  listaTipoComponenteDrop: any[];
  tipoComponenteSelected: any
  listaOtrosComponentesDrop: any[];
  esNuevo: boolean = true;

  tipoComponenteElegido: any
  componentePadreElegido: any

  registroId: number = -1
  resultadoExitoso: boolean = false


  ngOnInit() {
    this.registro = new ComponenteDTO();
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;

    this.obtenerTipoComponentes();
  }

  obtenerTipoComponentes() {
    this.listaTipoComponenteDrop = [];
    this.service.OtroGet('tipoComponente', '', null).subscribe((data: TipoComponenteDTO[]) => {
      if (data != null) {
        data.forEach(element => {
          this.listaTipoComponenteDrop.push({ name: element.nombre, code: element.tipoComponenteId });
        });
        this.obtenerOtrosComponentes();
      }
    }
    );
  }

  obtenerOtrosComponentes() {
    this.listaOtrosComponentesDrop = [];
    this.service.OtroGet('componente', '', null).subscribe((data: ComponenteDTO[]) => {
      if (data != null) {
        data.forEach(element => {
          if (element.componenteId != this.registroId)
            this.listaOtrosComponentesDrop.push({ name: element.nombreMostrar, code: element.componenteId });
        });
        if (!this.esNuevo)
          this.obtenerDetalleRegistro();
      }
    }
    );
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('componente', 'GetById', queryParams).subscribe((data: ComponenteDTO) => {
      if (data != null) {
        this.registro = data;
        this.tipoComponenteElegido = this.listaTipoComponenteDrop.find(x => x.code == this.registro.tipoComponenteId);
        this.componentePadreElegido = this.listaOtrosComponentesDrop.find(x => x.code == this.registro.padreId);
      }
    }
    );
  }

  guardarRegistro() {
    if (this.tipoComponenteElegido != null)
      this.registro.tipoComponenteId = this.tipoComponenteElegido.code;
    if (this.componentePadreElegido != null)
      this.registro.padreId = this.componentePadreElegido.code;

    this.resultadoExitoso = false;
    if (this.esNuevo)
      this.crearRegistro();
    else
      this.actualizarRegistro();
  }

  crearRegistro() {
    this.service.OtroPost('componente', '', this.registro).subscribe((data: ComponenteDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.service.OtroPut('componente', '', this.registro).subscribe((data: ComponenteDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro actualizado con éxito', showConfirmButton: false, timer: 3500, toast: true });
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  cerrarVentana() {
    this.ref.close();
  }

}
