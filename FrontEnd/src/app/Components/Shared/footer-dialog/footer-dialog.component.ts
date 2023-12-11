import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-footer-dialog',
  templateUrl: './footer-dialog.component.html',
  styleUrls: ['./footer-dialog.component.scss']
})
export class FooterDialogComponent implements OnInit {

  @Output() guardarEvent = new EventEmitter<boolean>();
  @Output() cancelarEvent = new EventEmitter<boolean>();

  @Input() showGuardar: boolean = true;

  constructor() { }

  ngOnInit() {
  }

  Guardar(){
    this.guardarEvent.emit(true);
  }
  Cancelar(){
    this.cancelarEvent.emit(true);
  }


}
