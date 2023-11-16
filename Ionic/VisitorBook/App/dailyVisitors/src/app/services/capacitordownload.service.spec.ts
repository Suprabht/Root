import { TestBed } from '@angular/core/testing';

import { CapacitordownloadService } from './capacitordownload.service';

describe('CapacitordownloadService', () => {
  let service: CapacitordownloadService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CapacitordownloadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
