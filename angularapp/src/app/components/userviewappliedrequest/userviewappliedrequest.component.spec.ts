import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { UserviewappliedrequestComponent } from './userviewappliedrequest.component';

describe('UserviewappliedrequestComponent', () => {
  let component: UserviewappliedrequestComponent;
  let fixture: ComponentFixture<UserviewappliedrequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserviewappliedrequestComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserviewappliedrequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_userviewappliedrequest_component', () => {
    expect(component).toBeTruthy();
  });
  fit('Frontend_should_display_heading_applied_training_requests_in_userviewappliedrequest_component', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Applied Training Requests');
  });
});
