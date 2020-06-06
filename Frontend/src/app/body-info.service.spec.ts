import { TestBed } from '@angular/core/testing';

import { BodyInfoService } from './body-info.service';

describe('BodyInfoServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BodyInfoService = TestBed.get(BodyInfoService);
    expect(service).toBeTruthy();
  });
});
