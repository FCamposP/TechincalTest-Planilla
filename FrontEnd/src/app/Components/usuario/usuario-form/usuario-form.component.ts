import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DialogService } from 'primeng/dynamicdialog';
import { EmpleadoDTO } from 'src/app/DTO/EmpleadoDTO';
import { RolDTO } from 'src/app/DTO/RolDTO';
import { UsuarioDTO } from 'src/app/DTO/UsuarioDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';
import { CambiarPasswordComponent } from '../cambiar-password/cambiar-password.component';

@Component({
  selector: 'app-usuario-form',
  templateUrl: './usuario-form.component.html',
  styleUrls: ['./usuario-form.component.scss']
})
export class UsuarioFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig, public dialogService: DialogService) { }

  registro: UsuarioDTO;
  submitted: boolean;
  registrosLista: UsuarioDTO[];
  empleadoSelected: any
  listaEmpleados: any[];
  esNuevo: boolean = true;
  registroId: number = -1
  resultadoExitoso: boolean = false
  usuarioRoles: RolDTO[];

  dynPassword: DynamicDialogRef;


  ngOnInit() {
    this.registro = new UsuarioDTO();
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;

    this.obtenerEmpleados();
  }

  obtenerEmpleados() {
    this.listaEmpleados = [];
    this.service.OtroGet('empleado', '', null).subscribe((data: EmpleadoDTO[]) => {
      if (data != null) {
        data.forEach(element => {
          this.listaEmpleados.push({ nombre: element.nombre, code: element.empleadoId });
        });
        this.obtenerRoles();
      }
    }
    );
  }

  obtenerRoles() {
    this.usuarioRoles = [];
    let queryParams = new HttpParams();
    queryParams = queryParams.append("userId", this.registroId);
    this.service.OtroGet('rol', 'GetRolesPorUsuario', queryParams).subscribe((data: RolDTO[]) => {
      if (data != null) {
        this.usuarioRoles = data;
        if (!this.esNuevo)
          this.obtenerDetalleRegistro();
      }
    }
    );
  }

  obtenerDetalleRegistro() {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('usuario', 'GetById', queryParams).subscribe((data: UsuarioDTO) => {
      if (data != null) {
        this.registro = data;
        this.empleadoSelected = this.listaEmpleados.find(x => x.code == this.registro.empleadoId);
      }
    }
    );
  }

  guardarRegistro() {
    if (this.empleadoSelected != null)
      this.registro.empleadoId = this.empleadoSelected.code;
    this.resultadoExitoso = false;
    if (this.esNuevo)
      this.crearRegistro();
    else
      this.actualizarRegistro();
  }

  crearRegistro() {
    this.service.OtroPost('usuario', '', this.registro).subscribe((data: UsuarioDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.service.OtroPut('usuario', '', this.registro).subscribe((data: UsuarioDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro actualizado con éxito', showConfirmButton: false, timer: 3500, toast: true });
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }


  cambiarPassword() {
    this.dynPassword = this.dialogService.open(CambiarPasswordComponent, {
      data: {
        usuario: this.registro,
      },
      header: "Cambiar password",
      width: '40%',
      height: '50%',
      styleClass: 'dynamicDialog'
    });

    this.dynPassword.onClose.subscribe((usarioConPassword: UsuarioDTO) => {
      if (usarioConPassword) {
        this.registro = usarioConPassword;
      }
    });
  }

  cerrarVentana() {
    this.ref.close();
  }

}
