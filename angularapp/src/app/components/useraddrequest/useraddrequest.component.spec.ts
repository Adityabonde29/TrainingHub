
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { UseraddrequestComponent } from './useraddrequest.component';

describe('UseraddrequestComponent', () => {
  let component: UseraddrequestComponent;
  let fixture: ComponentFixture<UseraddrequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UseraddrequestComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UseraddrequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_useraddrequest_component', () => {
    expect(component).toBeTruthy();
  });
  fit('Frontend_should_display_heading_training_request_form_in_useraddrequest_component', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Training Request Form');
  });
});
