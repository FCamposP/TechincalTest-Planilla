import { HttpParams } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DetallePlanillaDTO } from 'src/app/DTO/DetallePlanillaDTO';
import { EncabezadoPlanillaDTO } from 'src/app/DTO/EncabezadoPlanillaDTO';
import { EstadoPlanillaDTO } from 'src/app/DTO/EstadoPlanillaDTO';
import { PeriodoDTO } from 'src/app/DTO/PeriodoDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-cargar-planilla',
  templateUrl: './cargar-planilla.component.html',
  styleUrls: ['./cargar-planilla.component.scss']
})
export class CargarPlanillaComponent implements OnInit {

  registros: DetallePlanillaDTO[] = []; // Almacena los datos de Excel
  tamanioMaximo: number = 50000000;
  listaPeriodos: PeriodoDTO[]=[];
  listaEstados: EstadoPlanillaDTO[]=[];
  periodoSelected: PeriodoDTO;
  estadoSelected:EstadoPlanillaDTO;
  encabezado:EncabezadoPlanillaDTO;
  form: FormGroup;
  resultadoExitoso: boolean = false;
  esNuevo: boolean = true;
  registroId: number = -1;
  actualizarTabla:boolean = false;
  deshabilitar: boolean=false;
  constructor(private service: appServices<any>, public config: DynamicDialogConfig, private fb: FormBuilder,public ref: DynamicDialogRef) { }


  ngOnInit() {
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;
    this.obtenerEstados();
    this.form = this.fb.group({
      encabezadoPlanillaId: [-1],
      estadoPlanillaSelected: [new EstadoPlanillaDTO(), Validators.required],
      periodoActivoSelected:[new PeriodoDTO(), Validators.required],
    })

  }

  onUpload(event) {

    if (event.length === 0) {
      Swal.fire({ position: 'top-end', icon: 'info', text: 'No se ha cargado ningún documento', showConfirmButton: false, timer: 3500, toast: true });

      return;
    }
    var file = event.files[0];
    if (file.size <= this.tamanioMaximo) {
      const formData = new FormData();
      formData.append(file.name, file);

      this.service.CargarArchivos('planilla', 'PrecargaExcel', formData).subscribe((data: any[]) => {
        if (data) {
          this.registros=data;
        }
      }
      );
    } else {
      Swal.fire({ position: 'top-end', icon: 'info', text: 'El tamaño del archivo excede el límite permitido', showConfirmButton: false, timer: 3500, toast: true });
    }
  }

  clearData() {
    if (this.registros) {
      this.registros = [];
    }
  }

  obtenerEstados() {
    this.listaEstados = [];
    this.service.OtroGet('estadoPlanilla', '', null).subscribe((data: EstadoPlanillaDTO[]) => {
      if (data != null) {
        this.obtenerPeriodos();
        this.listaEstados=(data);

      }
    }
    );
  }

  obtenerPeriodos() {
    this.listaPeriodos = [];
    this.service.OtroGet('periodo', '', null).subscribe((data: PeriodoDTO[]) => {
      if (data != null) {
        this.listaPeriodos=data;
        if(!this.esNuevo){
          this.obtenerDetalleRegistro();
        }else{
          this.obtenerDatosIniciales();
        }
      }
    }
    );
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('planilla', 'GetById', queryParams).subscribe((data: EncabezadoPlanillaDTO) => {
      if (data != null) {
        this.encabezado = data;
        this.form.patchValue({
          encabezadoPlanillaId:data.encabezadoPlanillaId,
          estadoPlanillaSelected: this.listaEstados.find(x=>x.estadoPlanillaId==data.estadoPlanillaId),
          periodoActivoSelected:this.listaPeriodos.find(x=>x.periodoId==data.periodoId),
        });
        this.registros=this.encabezado.detallePlanilla;
        if(this.form.value.estadoPlanillaSelected.codigo=="APROBADO" || !data.habilitado){
          this.deshabilitar=true;
        }
      }
    }
    );
  }

  obtenerDatosIniciales(){
    this.service.OtroGet('planilla', 'ObtenerDatosIniciales', null).subscribe((data: EncabezadoPlanillaDTO) => {
      if (data != null) {
        this.form.patchValue({
          estadoPlanillaSelected: this.listaEstados.find(x=>x.estadoPlanillaId==data.estadoPlanillaId),
          periodoActivoSelected:this.listaPeriodos.find(x=>x.periodoId==data.periodoId),
        });
      }
    }
    );
  }

  onSubmit(){
    this.encabezado = {
      ...this.encabezado,
      periodoId:this.form.value.periodoActivoSelected.periodoId,
      estadoPlanillaId: this.form.value.estadoPlanillaSelected.estadoPlanillaId,
      detallePlanilla:this.registros
    }
    if(this.form.valid){
      this.guardarRegistro()
    }
  }

  guardarRegistro() {
    this.resultadoExitoso = false;
    if (this.esNuevo)
      this.crearRegistro();
    else
      this.actualizarRegistro();
  }

  crearRegistro() {
    this.service.OtroPost('planilla', '', this.encabezado).subscribe((data: boolean) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.encabezado.detallePlanilla=this.registros.filter(x=>x.actualizar==true);
    this.encabezado.detallePlanilla.forEach(element => {
      delete element.actualizar;
      element.salario=parseFloat(element.salario.toString());
      element.descuentoAfp=parseFloat(element.descuentoAfp.toString());
      element.descuentoIsss=parseFloat(element.descuentoIsss.toString());
      element.descuentoRenta=parseFloat(element.descuentoRenta.toString());
      element.otrosDescuentos=parseFloat(element.otrosDescuentos.toString());
      element.sueldoNeto=parseFloat(element.sueldoNeto.toString());
    });
    this.service.OtroPut('planilla', '', this.encabezado).subscribe((data: boolean) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro actualizado con éxito', showConfirmButton: false, timer: 3500, toast: true });
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }
}
