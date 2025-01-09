import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { AdminviewappliedrequestComponent } from './adminviewappliedrequest.component';

describe('AdminviewappliedrequestComponent', () => {
  let component: AdminviewappliedrequestComponent;
  let fixture: ComponentFixture<AdminviewappliedrequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminviewappliedrequestComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminviewappliedrequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  fit('Frontend_should_create_adminviewappliedrequest_component', () => {
    expect(component).toBeTruthy();
  });
});
