import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfiguracionGlobalFormComponent } from './configuracion-global-form.component';

describe('ConfiguracionGlobalFormComponent', () => {
  let component: ConfiguracionGlobalFormComponent;
  let fixture: ComponentFixture<ConfiguracionGlobalFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConfiguracionGlobalFormComponent]
    });
    fixture = TestBed.createComponent(ConfiguracionGlobalFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
