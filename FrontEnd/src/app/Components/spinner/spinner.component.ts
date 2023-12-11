import { Component } from '@angular/core';
import { SpinnerService } from 'src/app/Services/spinner.service';

@Component({
  selector: 'app-spinner',
  template: '<div *ngIf="isLoading | async" class="page-preloader"></div>',
  styleUrls: ['./spinner.component.scss'],
})

export class SpinnerComponent {
  
  constructor(private spinnerService: SpinnerService) { }
  isLoading:any;
  
  ngOnInit() {
  this.isLoading= this.spinnerService.isLoading;
  }

}
