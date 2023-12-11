import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { RolDTO, RolPermisoDTO, RolTree, RolUsuarioDTO } from 'src/app/DTO/RolDTO';
import { UsuarioDTO } from 'src/app/DTO/UsuarioDTO';
import { appServices } from 'src/app/Services/appServices';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-rol-formulario',
  templateUrl: './rol-form.component.html',
  styleUrls: ['./rol-form.component.scss']
})
export class RolFormComponent implements OnInit {

  constructor(private service: appServices<any>, public ref: DynamicDialogRef, public config: DynamicDialogConfig) { }

  registro: RolDTO;
  submitted: boolean;
  registrosLista: RolDTO[];

  listaTipoComponenteDrop: any[];
  tipoComponenteSelected: any
  listaOtrosComponentesDrop: any[];
  esNuevo: boolean = true;

  registroId: number = -1
  resultadoExitoso: boolean = false

  listaUsuario: UsuarioDTO[] = [];
  listaUsuarioBack: UsuarioDTO[] = [];
  usuariosRol: any[];
  usuarioElegido: UsuarioDTO;
  rolUsuarios: UsuarioDTO[] = [];

  componentesTree: RolTree[] = [];
  componentesSeleccionados: RolTree[] = [];

  tipoComponentes: SelectItem[];
  esFrontOffice: boolean = false;
  componentesFiltrados: string = "Back Office"

  ngOnInit() {
    this.registro = new RolDTO();
    this.registro.rolPermisos = [];
    this.registro.rolUsuarios = [];
    this.esNuevo = this.config.data.esNuevo;
    this.registroId = this.config.data.registroId;

    this.obtenerUsuarios();
    if (!this.esNuevo)
      this.obtenerDetalleRegistro();
    else
      this.obtenerPermisos(this.esFrontOffice);

    this.tipoComponentes = [
      { label: 'Front Office', value: true },
      { label: 'Back Office', value: false }
    ];
  }

  obtenerUsuarios() {
    this.listaUsuario = [];
    this.service.OtroGet('usuario', '', null).subscribe((data: UsuarioDTO[]) => {
      if (data != null) {
        this.listaUsuario = data;
        this.listaUsuarioBack = data;
      }
    }
    );
  }

  obtenerPermisos(esFrontOffice) {
    this.componentesTree = [];
    let queryParams = new HttpParams();
    queryParams = queryParams.append("esFrontOffice", esFrontOffice);
    this.service.OtroGet('rol', 'GetRolPermisoTree', queryParams).subscribe((data: RolTree[]) => {
      if (data != null) {
        this.componentesTree = data;
        this.componentesPermiso(this.componentesTree, this.registro.rolPermisos);
      }
    }
    );
  }

  componentesPermiso(componentesAnidados: RolTree[], rolComponentes: RolPermisoDTO[]) {
    componentesAnidados.forEach(componente => {
      var buscarComponenteConfigurado = rolComponentes.find(x => x.componenteId == Number(componente.key));
      if (buscarComponenteConfigurado != null) {
        this.componentesSeleccionados.push(componente);
      }
      this.componentesPermiso(componente.children, rolComponentes);
    });
  }

  obtenerDetalleRegistro() {
    this.registro = new RolDTO();
    let queryParams = new HttpParams();
    queryParams = queryParams.append("id", this.registroId);
    this.service.OtroGet('rol', 'GetById', queryParams).subscribe((data: RolDTO) => {
      if (data != null) {
        this.registro = data;
        var rolUsuariosBack = [];
        this.registro.rolUsuarios.forEach(rolUser => {
          this.listaUsuario = this.listaUsuario.filter(x => x.usuarioId != rolUser.usuarioId);
          var userFind = this.listaUsuarioBack.find(x => x.usuarioId == rolUser.usuarioId);
          if (userFind != null)
            rolUsuariosBack.push(userFind);
        });
        this.rolUsuarios = rolUsuariosBack;
        this.obtenerPermisos(this.esFrontOffice);
      }
    }
    );
  }

  guardarRegistro() {
    this.guardarRolUsuario();
    this.guardarRolPermiso();

    this.resultadoExitoso = false;
    if (this.esNuevo)
      this.crearRegistro();
    else
      this.actualizarRegistro();
  }

  guardarRolUsuario() {
    var rolUsuariosBack = this.registro.rolUsuarios;
    this.registro.rolUsuarios = [];
    this.rolUsuarios.forEach(element => {
      var agregarRolUsuario = new RolUsuarioDTO();
      agregarRolUsuario.usuarioId = Number(element.usuarioId);
      agregarRolUsuario.rolId = this.registro.rolId;
      var rolUsuario = rolUsuariosBack.find(x => x.usuarioId == element.usuarioId);
      agregarRolUsuario.rolUsuarioId = rolUsuario != null ? rolUsuario.rolUsuarioId : -1;
      this.registro.rolUsuarios.push(agregarRolUsuario);
    });
  }

  guardarRolPermiso() {
    this.registro.rolPermisos = [];
    this.componentesSeleccionados.forEach(element => {
      var agregarRolPermiso = new RolPermisoDTO();
      agregarRolPermiso.componenteId = Number(element.key);
      this.registro.rolPermisos.push(agregarRolPermiso);
    });
  }

  crearRegistro() {
    this.service.OtroPost('rol', '', this.registro).subscribe((data: RolDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro creado con éxito', showConfirmButton: false, timer: 3500, toast: true }); 4
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  actualizarRegistro() {
    this.service.OtroPut('rol', '', this.registro).subscribe((data: RolDTO) => {
      if (data != null) {
        Swal.fire({ position: 'top-end', icon: 'success', text: 'Registro actualizado con éxito', showConfirmButton: false, timer: 3500, toast: true });
        this.resultadoExitoso = true;
        this.ref.close(this.resultadoExitoso);
      }
    }
    );
  }

  agregarUsuario() {
    if (this.usuarioElegido != null) {
      this.listaUsuario = this.listaUsuario.filter(x => x.usuarioId != this.usuarioElegido.usuarioId);
      this.rolUsuarios.push(this.usuarioElegido);
      this.usuarioElegido = null;
    }
  }

  quitarUsuario(usuarioQuitar) {
    if (usuarioQuitar != null) {
      this.rolUsuarios = this.rolUsuarios.filter(x => x.usuarioId != usuarioQuitar.usuarioId);
      this.listaUsuario.push(usuarioQuitar);
    }
  }

  cerrarVentana() {
    this.ref.close();
  }

  cambioComponentes(esFrontOffice) {
    this.esFrontOffice = esFrontOffice;
    this.obtenerPermisos(this.esFrontOffice);
  }
}
