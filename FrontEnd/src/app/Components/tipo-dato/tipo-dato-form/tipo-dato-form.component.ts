import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DialogService } from 'primeng/dynamicdialog';
import { TipoDatoDTO } from 'src/app/DTO/TipoDatoDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-tipo-dato-form',
  templateUrl: './tipo-dato-form.component.html',
  styleUrls: ['./tipo-dato-form.component.scss']
})
export class TipoDatoFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig, public dialogService: DialogService, private fb: FormBuilder) { }

  registro: TipoDatoDTO;
  submitted: boolean;
  registrosLista: TipoDatoDTO[];
  esNuevo: boolean = true;
  registroId: number = -1;
  resultadoExitoso: boolean = false;
  form: FormGroup;

  dynPassword: DynamicDialogRef;

  ngOnInit() {
    this.registro = new TipoDatoDTO();
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;

    if(!this.esNuevo){
      this.obtenerDetalleRegistro();
    }

    this.form = this.fb.group({
      codigo: ['', Validators.required],
      nombre: ['', Validators.required],
      descripcion: ['', ],
      formato: ['', ]
    })
  }

  onSubmit(form: FormGroup){
    this.registro = {
      ...this.registro,
      codigo: form.value.codigo,
      nombre: form.value.nombre,
      descripcion: form.value.descripcion,
      formato: form.value.formato
    }
    if(form.valid){
      this.guardarRegistro()
    }
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('tipodato', 'GetById', queryParams).subscribe((data: TipoDatoDTO) => {
      if (data != null) {
        this.registro = data;

        this.form.patchValue({
          codigo: this.registro.codigo,
          nombre: this.registro.nombre,
          descripcion: this.registro.descripcion,
          formato: this.registro.formato
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
    this.service.OtroPost('tipodato', '', this.registro).subscribe((data: TipoDatoDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.service.OtroPut('tipodato', '', this.registro).subscribe((data: TipoDatoDTO) => {
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
