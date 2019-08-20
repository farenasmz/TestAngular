import { TestBed, inject } from '@angular/core/testing';

import { RegularGuardService } from './regular-guard.service';

describe('RegularGuardService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RegularGuardService]
    });
  });

  it('should be created', inject([RegularGuardService], (service: RegularGuardService) => {
    expect(service).toBeTruthy();
  }));
});
