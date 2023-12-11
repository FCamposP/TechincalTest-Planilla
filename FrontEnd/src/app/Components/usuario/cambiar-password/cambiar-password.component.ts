import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UsuarioDTO } from 'src/app/DTO/UsuarioDTO';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import Validation from 'src/app/utilities/validation';

@Component({
  selector: 'app-cambiar-password',
  templateUrl: './cambiar-password.component.html',
  styleUrls: ['./cambiar-password.component.scss']
})
export class CambiarPasswordComponent implements OnInit {

  constructor(public ref: DynamicDialogRef, public config: DynamicDialogConfig,private formBuilder: FormBuilder) { }


  submitted: boolean=false;
  registro: UsuarioDTO;
  form: FormGroup;
  ngOnInit() {
    this.registro = new UsuarioDTO();
    this.registro=this.config.data.usuario;

    this.form = this.formBuilder.group(
      {
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(40)
          ]
        ],
        confirmPassword: ['', Validators.required],
      },
      {
        validators: [Validation.match('password', 'confirmPassword')]
      }
    );
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.guardarRegistro();
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }

  guardarRegistro(){
    this.registro.password=this.form.value.password;
    this.registro.passwordConfirmacion=this.form.value.confirmPassword;
    this.registro.actualizarPassword=true;
    this.ref.close(this.registro);

  }

  cerrarVentana() {
    this.ref.close();
  }


}
