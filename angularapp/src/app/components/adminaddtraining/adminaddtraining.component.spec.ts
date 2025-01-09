
import { AdminaddtrainingComponent } from './adminaddtraining.component';

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';


describe('AdminaddtrainingComponent', () => {
  let component: AdminaddtrainingComponent;
  let fixture: ComponentFixture<AdminaddtrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminaddtrainingComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminaddtrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_adminaddtraining_component', () => {
    expect(component).toBeTruthy();
  });
  fit('Frontend_should_contain_create_new_training_heading_in_the_adminaddtraining_component', () => {
    const componentHTML = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Create New Training');
  });
});
