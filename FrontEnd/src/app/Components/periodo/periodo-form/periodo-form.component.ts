import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DialogService } from 'primeng/dynamicdialog';
import { PeriodoDTO } from 'src/app/DTO/PeriodoDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-periodo-form',
  templateUrl: './periodo-form.component.html',
  styleUrls: ['./periodo-form.component.scss']
})
export class PeriodoFormComponent implements OnInit{

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig, public dialogService: DialogService, private fb: FormBuilder) { }

  registro: PeriodoDTO;
  submitted: boolean;
  registrosLista: PeriodoDTO[];

  esNuevo: boolean = true;
  registroId: number = -1;
  resultadoExitoso: boolean = false;
  form: FormGroup;
  fechaInicio: Date;
  fechaFin: Date;


  dynPassword: DynamicDialogRef;

  ngOnInit() {
    this.registro = new PeriodoDTO();
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;

    // this.fechaFin= new Date();

    if(!this.esNuevo){
      this.obtenerDetalleRegistro();
    }

    this.form = this.fb.group({
      fechaInicio: ['', Validators.required],
      fechaFin: ['', Validators.required],
      habilitado: ['', Validators.required]
    })
  }
  
  onSubmit(form: FormGroup){
    this.registro = {
      ...this.registro,
      tipoPeriodoId: form.value.tipoPeriodoSelected.tipoPeriodoId,
      fechaInicio: form.value.fechaInicio,
      fechaFin: form.value.fechaFin,
      habilitado: form.value.habilitado
    }
    if(form.valid){
      this.guardarRegistro()
    }
  }


  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('periodo', 'GetById', queryParams).subscribe((data: PeriodoDTO) => {
      if (data != null) {
        this.registro = data;
        this.fechaFin= new Date(this.registro.fechaFin);
        this.fechaInicio= new Date(this.registro.fechaInicio);
        this.form.patchValue({
          fechaInicio: new Date(this.registro.fechaInicio),
          fechaFin: new Date(this.registro.fechaFin),
          habilitado: this.registro.habilitado
        });
      }
    }
    );
  }

  guardarRegistro() {

    if (this.registro.fechaInicio==null) {
      this.openAlert('Quien es quien', 'Debe de seleccionar una fecha de inicio valida', 'error');
      return;
    }
    if (this.registro.fechaFin==null) {
      this.openAlert('Quien es quien', 'Debe de seleccionar una fecha de fin valida', 'error');
      return;
    }
    if (this.registro.fechaFin < this.registro.fechaInicio) {
      this.openAlert('Quien es quien', 'La fecha fin es menor a la fecha de inicio', 'error');
      return;
    }

    this.resultadoExitoso = false;
    if (this.esNuevo)
      this.crearRegistro();
    else
      this.actualizarRegistro();
  }

  crearRegistro() {
    this.service.OtroPost('periodo', '', this.registro).subscribe((data: PeriodoDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.service.OtroPut('periodo', '', this.registro).subscribe((data: PeriodoDTO) => {
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

  openAlert(title: string, texto: string, icon: any) {
    Swal.fire({ position: 'top-end', icon: icon, text: texto, showConfirmButton: false, timer: 3500, toast: true });

  }

}
