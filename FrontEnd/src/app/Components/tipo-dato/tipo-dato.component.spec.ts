import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoDatoComponent } from './tipo-dato.component';

describe('TipoDatoComponent', () => {
  let component: TipoDatoComponent;
  let fixture: ComponentFixture<TipoDatoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TipoDatoComponent]
    });
    fixture = TestBed.createComponent(TipoDatoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
