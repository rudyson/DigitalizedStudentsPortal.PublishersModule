import { TestBed } from '@angular/core/testing';

import { MicrosoftAuthorizationControllerService } from './microsoft-authorization-controller.service';

describe('MicrosoftAuthorizationControllerService', () => {
  let service: MicrosoftAuthorizationControllerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MicrosoftAuthorizationControllerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
