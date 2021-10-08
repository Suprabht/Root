import { TestBed } from '@angular/core/testing';

import { VisitorsDetailsService } from './visitors-details.service';

describe('VisitorsDetailsService', () => {
  let service: VisitorsDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VisitorsDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
