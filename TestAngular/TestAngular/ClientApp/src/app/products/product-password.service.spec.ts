import { TestBed, inject } from '@angular/core/testing';

import { ProductPasswordService } from './product-password.service';

describe('ProductPasswordService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProductPasswordService]
    });
  });

  it('should be created', inject([ProductPasswordService], (service: ProductPasswordService) => {
    expect(service).toBeTruthy();
  }));
});
