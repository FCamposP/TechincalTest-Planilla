import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DialogService } from 'primeng/dynamicdialog';
import { TipoComponenteDTO } from 'src/app/DTO/TipoComponenteDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-tipo-componente-form',
  templateUrl: './tipo-componente-form.component.html',
  styleUrls: ['./tipo-componente-form.component.scss']
})
export class TipoComponenteFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig, public dialogService: DialogService, private fb: FormBuilder) { }

  registro: TipoComponenteDTO;
  submitted: boolean;
  registrosLista: TipoComponenteDTO[];
  esNuevo: boolean = true;
  registroId: number = -1;
  resultadoExitoso: boolean = false;
  form: FormGroup;

  dynPassword: DynamicDialogRef;

  ngOnInit() {
    this.registro = new TipoComponenteDTO();
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;

    if(!this.esNuevo){
      this.obtenerDetalleRegistro();
    }

    this.form = this.fb.group({
      codigo: ['', Validators.required],
      nombre: ['', Validators.required],
      descripcion: ['', Validators.required]
    })
  }

  onSubmit(form: FormGroup){
    this.registro = {
      ...this.registro,
      codigo: form.value.codigo,
      nombre: form.value.nombre,
      descripcion: form.value.descripcion
    }
    if(form.valid){
      this.guardarRegistro()
    }
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('tipocomponente', 'GetById', queryParams).subscribe((data: TipoComponenteDTO) => {
      if (data != null) {
        this.registro = data;

        this.form.patchValue({
          codigo: this.registro.codigo,
          nombre: this.registro.nombre,
          descripcion: this.registro.descripcion
        });
      }
    }
    );
  }

  guardarRegistro() {
    this.resultadoExitoso = false;
    if (this.esNuevo)
      this.crearRegistro();
    else
      this.actualizarRegistro();
  }

  crearRegistro() {
    this.service.OtroPost('tipocomponente', '', this.registro).subscribe((data: TipoComponenteDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.service.OtroPut('tipocomponente', '', this.registro).subscribe((data: TipoComponenteDTO) => {
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
