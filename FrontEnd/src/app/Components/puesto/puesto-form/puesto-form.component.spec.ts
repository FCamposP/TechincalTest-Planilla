import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PuestoFormComponent } from './puesto-form.component';

describe('PuestoFormComponent', () => {
  let component: PuestoFormComponent;
  let fixture: ComponentFixture<PuestoFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PuestoFormComponent]
    });
    fixture = TestBed.createComponent(PuestoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
