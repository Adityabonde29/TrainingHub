
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { AdminviewtrainingComponent } from './adminviewtraining.component';

describe('AdminviewtrainingComponent', () => {
  let component: AdminviewtrainingComponent;
  let fixture: ComponentFixture<AdminviewtrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminviewtrainingComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminviewtrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_adminviewtraining_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_training_sessions_heading_in_the_adminviewtraining_component', () => {
    const componentHTML = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Training Sessions');
  });
});
