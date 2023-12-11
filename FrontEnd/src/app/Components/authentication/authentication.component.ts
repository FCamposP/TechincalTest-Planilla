import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { appServices } from 'src/app/Services/appServices';
import { SharedService } from 'src/app/Services/sharedService';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/Services/authservice';
import { LoginDTO } from 'src/app/DTO/LoginDTO';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss']
})
export class AuthenticationComponent implements OnInit {

  constructor(private shared: SharedService, private service: appServices<any>,private authService: AuthService, private router: Router, private fb: FormBuilder) { }

  form: FormGroup;
  errorLogin: boolean = false;

  ngOnInit(): void {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    })
  }

  onSubmit(form: FormGroup){
    const payload : LoginDTO = {
      username: form.value.username,
      password: form.value.password
    };

    if(form.valid){
      this.onLogin(payload)
    }
  }

  onLogin = (user: LoginDTO) => {
    this.errorLogin = false;

      this.authService.loginService(user)
      .subscribe({
        next:(data) => {
          if(data.data != null){
            localStorage.setItem('access_token', data.data.tokenResponse.accessToken);
            localStorage.setItem('refresh_token', data.data.tokenResponse.refreshToken);
            localStorage.setItem('expire_at', data.data.tokenResponse.expireAt);
            localStorage.setItem('userId', data.data.userId);
            let queryParams = new HttpParams();
            queryParams = queryParams.append("userId", data.data.userId);
            this.service.OtroGet('rolUser', 'GetRolesPorUsuario', queryParams).subscribe((data: any) => {
              if (data != null) {
                this.shared.PermisosOpciones = JSON.stringify(data);
                this.errorLogin = false;
                this.router.navigate(['mantenimiento']);
              }
            }
            );
          }
          else{
            this.errorLogin = true;
          }
        }
      })
  }

}
