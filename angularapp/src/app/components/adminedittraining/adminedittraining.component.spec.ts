import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminedittrainingComponent } from './adminedittraining.component';

describe('AdminedittrainingComponent', () => {
  let component: AdminedittrainingComponent;
  let fixture: ComponentFixture<AdminedittrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminedittrainingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminedittrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
