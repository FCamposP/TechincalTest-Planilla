import {Component} from '@angular/core';
import {AppComponent} from './app.component';

@Component({
    selector: 'app-footer',
    template: `
        <div class="layout-footer">
            <span class="footer-text-left">
                <img [src]="'assets/layout/images/logo' + (app.layoutColor === 'light' ? '-dark' : '') + '.png'" />
            </span>
            <span class="footer-text-right">
                <a href="#"><i class="pi pi-facebook"></i></a>
                <a href="#"><i class="pi pi-twitter"></i></a>
                <a href="#"><i class="pi pi-github"></i></a>
            </span>
        </div>
    `
})
export class AppFooterComponent {
    constructor(public app: AppComponent) {}
}
