import { Component, OnInit } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { DynamicDialogRef, DynamicDialogConfig, DialogService } from 'primeng/dynamicdialog';
import { TipoDatoDTO } from 'src/app/DTO/TipoDatoDTO';
import { ColumnaExcelDTO } from 'src/app/DTO/ColumnaExcelDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-columna-excel-form',
  templateUrl: './columna-excel-form.component.html',
  styleUrls: ['./columna-excel-form.component.scss']
})
export class ColumnaExcelFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig, public dialogService: DialogService, private fb: FormBuilder) { }

  registro: ColumnaExcelDTO;
  submitted: boolean;
  registrosLista: ColumnaExcelDTO[];
  tipoDatoSelected: TipoDatoDTO;
  tipoDatos: TipoDatoDTO[];
  esNuevo: boolean = true;
  registroId: number = -1;
  resultadoExitoso: boolean = false;
  form: FormGroup;

  dynPassword: DynamicDialogRef;

  ngOnInit() {
    this.registro = new ColumnaExcelDTO();
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;

    this.obtenerTipoDatos();


    this.form = this.fb.group({
      columnaExcelId: [0, ],
      tipoDatoSelected: [new TipoDatoDTO(), Validators.required],
      nombre: ['', Validators.required],
      descripcion: ['', Validators.required],
    })

  }

  onSubmit(form: FormGroup){
    this.registro = {
      ...this.registro,
      columnaExcelId: form.value.columnaExcelId,
      tipoDatoId: form.value.tipoDatoSelected.tipoDatoId,
      nombre: form.value.nombre,
      descripcion: form.value.descripcion,
    }
    if(form.valid){
      this.guardarRegistro()
    }
  }

  obtenerTipoDatos() {
    this.tipoDatos = [];
    this.service.OtroGet('tipodato', '', null).subscribe((data: TipoDatoDTO[]) => {
      if (data != null) {
        
        data.forEach(element => {
          this.tipoDatos.push(element);
        });

        if(!this.esNuevo){
          this.obtenerDetalleRegistro();
        }
      }
    }
    );
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('columnaExcel', 'GetById', queryParams).subscribe((data: ColumnaExcelDTO) => {
      if (data != null) {
        this.registro = data;
        this.tipoDatoSelected = this.tipoDatos.find(x => x.tipoDatoId == this.registro.tipoDatoId);

        this.form.patchValue({
          columnaExcelId: this.registro.columnaExcelId,
          tipoDatoSelected: this.tipoDatoSelected,
          nombre: this.registro.nombre,
          descripcion: this.registro.descripcion,
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
    this.service.OtroPost('columnaExcel', '', this.registro).subscribe((data: ColumnaExcelDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.service.OtroPut('columnaExcel', '', this.registro).subscribe((data: ColumnaExcelDTO) => {
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
