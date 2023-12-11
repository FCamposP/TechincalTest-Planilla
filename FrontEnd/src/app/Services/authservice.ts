import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";
import { Router } from "@angular/router";
import { LoginDTO } from "../DTO/LoginDTO";

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private loginURL: string;
    private refreshLogin: string;

    constructor(private http: HttpClient, private router: Router) {
        this.loginURL = environment.apiUrl + "Auth/Login";
        this.refreshLogin = environment.apiUrl + "Auth/Refresh";
    }

    loginService = (payload: LoginDTO): Observable<any> => {
        return this.http.post<any>(this.loginURL, payload, {
            headers: {
                ContentType: "application/json",
            },
        })
    };
    

    refreshTokenService = () => {
        return this.http.get<any>(this.refreshLogin, {
            headers: {
                "refresh_token": localStorage.getItem("refresh_token")
            }
        });
    }

    logoutService = () => {
        const token = localStorage.getItem('access_token');
        try{
            if(token !== null || token !== undefined){
                localStorage.removeItem('access_token');
                localStorage.removeItem('refresh_token');
                localStorage.removeItem('expire_at');
                localStorage.removeItem('userId');
                this.router.navigate([''])
            }
        }
        catch(error){
        }
    }

    isAuthenticated = () : boolean => {
        let bool : boolean;
        if(localStorage['access_token']){
            localStorage.getItem('access_token') !== null ? bool = true : bool = false;
        }
        return bool;
    }

    hasRefreshToken(): boolean {
        return localStorage.getItem('refreshToken') !== null;
      }
}
