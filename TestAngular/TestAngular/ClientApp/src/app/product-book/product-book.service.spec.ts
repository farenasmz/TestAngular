import { TestBed, inject } from '@angular/core/testing';

import { ProductBookService } from './product-book.service';

describe('ProductBookService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProductBookService]
    });
  });

  it('should be created', inject([ProductBookService], (service: ProductBookService) => {
    expect(service).toBeTruthy();
  }));
});
