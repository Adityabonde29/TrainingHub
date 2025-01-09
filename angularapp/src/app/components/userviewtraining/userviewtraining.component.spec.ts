import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { UserviewtrainingComponent } from './userviewtraining.component';

describe('UserviewtrainingComponent', () => {
  let component: UserviewtrainingComponent;
  let fixture: ComponentFixture<UserviewtrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserviewtrainingComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule],

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserviewtrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_userviewtraining_component', () => {
    expect(component).toBeTruthy();
  });
  fit('Frontend_should_display_heading_available_trainings_in_userviewtraining_component', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Available Trainings');
  });
});
