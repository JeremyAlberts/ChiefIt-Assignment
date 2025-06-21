import { TestBed } from '@angular/core/testing';

import { YakshopService } from './yakshop.service';

describe('YakshopService', () => {
  let service: YakshopService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(YakshopService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
