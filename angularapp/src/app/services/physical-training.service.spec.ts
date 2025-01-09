import { TestBed } from '@angular/core/testing';

import { PhysicalTrainingService } from './physical-training.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('PhysicalTrainingService', () => {
  let service: PhysicalTrainingService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(PhysicalTrainingService);
  });

  fit('Frontend_should_create_physical_training_service', () => {
    expect(service).toBeTruthy();
  });
});
