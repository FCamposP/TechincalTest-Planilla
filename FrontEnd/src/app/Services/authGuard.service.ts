import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { SharedService } from "./sharedService";
import { AuthService } from './authservice';
import { Location } from '@angular/common';

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate {

    constructor(
        private auth: AuthService,
        private router: Router, private sharedService: SharedService, private location: Location,
        private activatedRoute: ActivatedRoute
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        const bool = this.auth.isAuthenticated();

        if (!bool) {
            this.router.navigate(['']);
            return false;
        }

        let tienePermiso: boolean = false;
        let ArrayDireccion = route.url;
        let direccion = '';
        
        direccion = ArrayDireccion[0].path;
        tienePermiso = this.sharedService.buscarPermiso(direccion);
        if (!tienePermiso) {
            this.router.navigate(['/mantenimiento/notfound']);
        }

        return true
    }

}