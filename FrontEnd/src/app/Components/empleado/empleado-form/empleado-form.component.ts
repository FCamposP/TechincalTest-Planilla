import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DialogService } from 'primeng/dynamicdialog';
import { PuestoDTO } from 'src/app/DTO/PuestoDTO';
import { EmpleadoDTO } from 'src/app/DTO/EmpleadoDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-empleado-form',
  templateUrl: './empleado-form.component.html',
  styleUrls: ['./empleado-form.component.scss']
})
export class EmpleadoFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig, public dialogService: DialogService, private fb: FormBuilder) { }

  registro: EmpleadoDTO;
  submitted: boolean;
  registrosLista: EmpleadoDTO[];
  puestoSelected: PuestoDTO;
  listaPuestos: PuestoDTO[];
  esNuevo: boolean = true;
  registroId: number = -1;
  resultadoExitoso: boolean = false;
  form: FormGroup;

  dynPassword: DynamicDialogRef;

  ngOnInit() {
    this.registro = new EmpleadoDTO();
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;

    this.obtenerPuestos();
    if(!this.esNuevo){
      this.obtenerDetalleRegistro();
    }

    this.form = this.fb.group({
      puestoSelected: [new PuestoDTO(), Validators.required],
      codigo:[''],
      primerNombre: ['', Validators.required],
      segundoNombre: ['', ],
      primerApellido: ['', Validators.required],
      segundoApellido: ['', ],
      email: ['', [Validators.required, Validators.email] ],
      telefono: ['', Validators.required],
    })

  }

  onSubmit(form: FormGroup){
    this.registro = {
      ...this.registro,
      codigo:form.value.codigo,
      puestoId: form.value.puestoSelected.puestoId,
      primerNombre: form.value.primerNombre,
      segundoNombre: form.value.segundoNombre,
      primerApellido: form.value.primerApellido,
      segundoApellido: form.value.segundoApellido,
      email: form.value.email,
      telefono: form.value.telefono.toString()
    }
    if(form.valid){
      this.guardarRegistro()
    }
  }

  obtenerPuestos() {
    this.listaPuestos = [];
    this.service.OtroGet('puesto', '', null).subscribe((data: PuestoDTO[]) => {
      if (data != null) {
        data.forEach(element => {
          this.listaPuestos.push({ nombre: element.nombre, descripcion: element.descripcion, activo: element.activo, puestoId: element.puestoId });
        });
      }
    }
    );
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('empleado', 'GetById', queryParams).subscribe((data: EmpleadoDTO) => {
      if (data != null) {
        this.registro = data;
        this.puestoSelected = this.listaPuestos.find(x => x.puestoId == this.registro.puestoId);

        this.form.patchValue({
          puestoSelected: this.puestoSelected,
          codigo:this.registro.codigo,
          primerNombre: this.registro.primerNombre,
          segundoNombre: this.registro.segundoNombre,
          primerApellido: this.registro.primerApellido,
          segundoApellido: this.registro.segundoApellido,
          email: this.registro.email,
          telefono: this.registro.telefono,
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
    this.service.OtroPost('empleado', '', this.registro).subscribe((data: EmpleadoDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.service.OtroPut('empleado', '', this.registro).subscribe((data: EmpleadoDTO) => {
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
