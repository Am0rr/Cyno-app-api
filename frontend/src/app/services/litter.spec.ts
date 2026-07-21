import { TestBed } from '@angular/core/testing';

import { Litter } from './litter';

describe('Litter', () => {
  let service: Litter;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Litter);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
