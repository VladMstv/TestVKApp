/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { VkReportServiceService } from './vk-report-service.service';

describe('Service: VkReportService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VkReportServiceService]
    });
  });

  it('should ...', inject([VkReportServiceService], (service: VkReportServiceService) => {
    expect(service).toBeTruthy();
  }));
});
