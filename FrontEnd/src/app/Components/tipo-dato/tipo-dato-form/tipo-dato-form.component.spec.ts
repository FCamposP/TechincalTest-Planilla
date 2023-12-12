import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoDatoFormComponent } from './tipo-dato-form.component';

describe('TipoDatoFormComponent', () => {
  let component: TipoDatoFormComponent;
  let fixture: ComponentFixture<TipoDatoFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TipoDatoFormComponent]
    });
    fixture = TestBed.createComponent(TipoDatoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
