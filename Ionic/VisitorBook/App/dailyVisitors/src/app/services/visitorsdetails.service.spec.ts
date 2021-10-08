import { TestBed } from '@angular/core/testing';

import { VisitorsdetailsService } from './visitorsdetails.service';

describe('VisitorsdetailsService', () => {
  let service: VisitorsdetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VisitorsdetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
