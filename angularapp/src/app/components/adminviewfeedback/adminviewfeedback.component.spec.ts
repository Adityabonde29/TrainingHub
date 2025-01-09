
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { AdminviewfeedbackComponent } from './adminviewfeedback.component';

describe('AdminviewfeedbackComponent', () => {
  let component: AdminviewfeedbackComponent;
  let fixture: ComponentFixture<AdminviewfeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminviewfeedbackComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminviewfeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_adminviewfeedback_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component', () => {
    const componentHTML = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Feedback Details');
  });
});
