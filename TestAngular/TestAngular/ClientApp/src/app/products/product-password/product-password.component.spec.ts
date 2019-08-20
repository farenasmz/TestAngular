import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductPasswordComponent } from './product-password.component';

describe('ProductPasswordComponent', () => {
  let component: ProductPasswordComponent;
  let fixture: ComponentFixture<ProductPasswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductPasswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
