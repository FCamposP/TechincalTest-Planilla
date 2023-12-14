import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

// componentes propios
import { HomeComponent } from './Components/home/home.component';

// fin de componentes propios

import { EmpleadoComponent } from './Components/empleado/empleado.component';
import { AppMainComponent } from './app.main.component';
import { PuestoComponent } from './Components/puesto/puesto.component';
import { PeriodoComponent } from './Components/periodo/periodo.component';
import { RolComponent } from './Components/rol/rol.component';
import { AuthenticationComponent } from './Components/authentication/authentication.component';
import { TipoComponenteComponent } from './Components/tipo-componente/tipo-componente.component';
import { AuthGuard } from './Services/authGuard.service';
import { LoginGuard } from './Services/loginGuard.service';
import { UsuarioComponent } from './Components/usuario/usuario.component';
import { ComponentesComponent } from './Components/componentes/componentes.component';
import { ConfiguracionGlobalComponent } from './Components/configuracion-global/configuracion-global.component';
import { AppNotfoundComponent } from '../app/Components/not-found/app.notfound.component';
import { ErrorLogComponent } from './Components/error-log/error-log.component';
import { TipoDatoComponent } from './Components/tipo-dato/tipo-dato.component';
import { ColumnaExcelComponent } from './Components/columna-excel/columna-excel.component';
import { PlanillaPagosComponent } from './Components/planilla-pagos/planilla-pagos.component';
import { BoletaPagoComponent } from './Components/boleta-pago/boleta-pago.component';

@NgModule({
    imports: [
        RouterModule.forRoot([
            {
                path: '',
                component: AuthenticationComponent,
                canActivate: [LoginGuard]
            },
            {
                path: '',
                component: AppMainComponent,                
                children: [
                    { path: '', component: HomeComponent },
                    { path: 'home', component: HomeComponent },
                    { path: 'puesto', component: PuestoComponent ,canActivate: [AuthGuard]},
                    { path: 'empleados', component: EmpleadoComponent ,canActivate: [AuthGuard]},
                    { path: 'periodo', component: PeriodoComponent ,canActivate: [AuthGuard]},
                    { path: 'rol', component: RolComponent ,canActivate: [AuthGuard]},
                    { path: 'componentes', component: ComponentesComponent ,canActivate: [AuthGuard]},
                    { path: 'usuario', component: UsuarioComponent ,canActivate: [AuthGuard]},
                    { path: 'tipo-componente', component: TipoComponenteComponent ,canActivate: [AuthGuard]},
                    { path: 'tipo-dato', component: TipoDatoComponent ,canActivate: [AuthGuard]},
                    { path: 'configuracion-global', component: ConfiguracionGlobalComponent, canActivate: [AuthGuard]},
                    { path: 'error-logs', component: ErrorLogComponent, canActivate: [AuthGuard]},
                    { path: 'columnas-excel', component: ColumnaExcelComponent, canActivate: [AuthGuard]},
                    { path: 'planilla-pagos', component: PlanillaPagosComponent, canActivate: [AuthGuard]},
                    { path: 'boletas-pagos', component: BoletaPagoComponent, canActivate: [AuthGuard]},
                    
                    { path: 'notfound', component: AppNotfoundComponent},
                    
                ]
            },
            { path: '**', redirectTo: '/notfound' },
        ], { scrollPositionRestoration: 'enabled' })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
