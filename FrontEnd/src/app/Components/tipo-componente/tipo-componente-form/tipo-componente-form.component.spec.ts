import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoComponenteFormComponent } from './tipo-componente-form.component';

describe('TipoComponenteFormComponent', () => {
  let component: TipoComponenteFormComponent;
  let fixture: ComponentFixture<TipoComponenteFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TipoComponenteFormComponent]
    });
    fixture = TestBed.createComponent(TipoComponenteFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
