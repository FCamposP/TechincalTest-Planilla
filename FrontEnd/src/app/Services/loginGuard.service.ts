import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './authservice';

@Injectable({
    providedIn: 'root'
})

export class LoginGuard implements CanActivate {

    constructor(
        private auth: AuthService,
        private router: Router,
    ){}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        const bool = this.auth.isAuthenticated();
        if(bool){
            this.router.navigate(['mantenimiento']);
            return false;
        }
        return true;
    }

}