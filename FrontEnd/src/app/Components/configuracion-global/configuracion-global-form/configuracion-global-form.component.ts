import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DialogService } from 'primeng/dynamicdialog';
import { ConfiguracionGlobalDTO } from 'src/app/DTO/ConfiguracionGlobalDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-configuracion-global-form',
  templateUrl: './configuracion-global-form.component.html',
  styleUrls: ['./configuracion-global-form.component.scss']
})
export class ConfiguracionGlobalFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig, public dialogService: DialogService, private fb: FormBuilder) { }

  registro: ConfiguracionGlobalDTO;
  submitted: boolean;
  registrosLista: ConfiguracionGlobalDTO[];
  esNuevo: boolean = true;
  registroId: number = -1;
  resultadoExitoso: boolean = false;
  form: FormGroup;

  imageBoolean: boolean = false;

  dynPassword: DynamicDialogRef;

  ngOnInit() {
    this.registro = new ConfiguracionGlobalDTO();
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;

    if (!this.esNuevo) {
      this.obtenerDetalleRegistro();
    }

    this.form = this.fb.group({
      codigo: ['', Validators.required],
      valor: ['', Validators.required],
      nombre: ['', Validators.required],
      esImagen: false
    })

    this.form.get('esImagen').valueChanges.subscribe(value => {
      if (value) {
        // this.form.get('codigo').disable();
        // this.form.get('codigo').setValue('RUTARECOMENDACIONIMAGEN');
      } else {
        // this.form.get('codigo').enable();
        // this.form.get('codigo').setValue('');
      }
    });

  }

  onSubmit(form: FormGroup) {
    this.registro = {
      ...this.registro,
      codigo: form.get("codigo").value,
      valor: form.get("valor").value,
      nombreAmostrar: form.value.nombre,
      esImagen: form.value.esImagen
    }
    if (form.valid) {
      this.guardarRegistro()
    }
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('configuracionglobal', 'GetById', queryParams).subscribe((data: ConfiguracionGlobalDTO) => {
      if (data != null) {
        this.registro = data;

        this.form.patchValue({
          codigo: this.registro.codigo,
          valor: this.registro.valor,
          nombre: this.registro.nombreAmostrar
        });

        this.form.get('codigo').disable();
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
    this.service.OtroPost('configuracionglobal', '', this.registro).subscribe((data: ConfiguracionGlobalDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
      else {
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.service.OtroPut('configuracionglobal', '', this.registro).subscribe((data: ConfiguracionGlobalDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro actualizado con éxito', showConfirmButton: false, timer: 3500, toast: true });
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  onUpload(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      const file: File = fileInput.files[0];
      if (file && this.isFileImage(file)) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.form.get('valor').setValue(e.target.result.split(',')[1]);
        };
        reader.readAsDataURL(file);
      } else {
        this.clearFileInput(fileInput);
        Swal.fire({ position: 'top-end', icon: 'error', text: 'Debe seleccionar una imagen', showConfirmButton: false, timer: 3500, toast: true });
      }
    }
    else {
      this.form.get('valor').setValue("");
    }
  }

  isFileImage(file: File): boolean {
    const allowedTypes = ["image/jpeg", "image/png", "image/jpg"];
    return allowedTypes.includes(file.type);
  }

  clearFileInput(input: HTMLInputElement): void {
    input.value = '';
  }

  cerrarVentana() {
    this.form.get('codigo').enable();
    this.ref.close();
  }

}
