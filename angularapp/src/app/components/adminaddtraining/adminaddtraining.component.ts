import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PhysicalTrainingService } from 'src/app/services/physical-training.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
 
@Component({
  selector: 'app-adminaddtraining',
  templateUrl: './adminaddtraining.component.html',
  styleUrls: ['./adminaddtraining.component.css']
})
export class AdminaddtrainingComponent implements OnInit {
   
  physicalTrainingForm: FormGroup;
  formSubmitted: boolean = false;
  isIndoor:string="false";
 
  constructor(private fb: FormBuilder, private pts: PhysicalTrainingService, private router: Router, private authService:AuthService) {
    this.physicalTrainingForm = this.fb.group({
      TrainingName: ['', Validators.required],
      Description: ['', Validators.required],
      TrainerName: ['', Validators.required],
      Location: ['', Validators.required],
      IsIndoor: ['', Validators.required],
      Fee: ['', [Validators.required, Validators.min(0)]],
      FocusArea: ['', Validators.required],
      PhysicalRequirements: ['', Validators.required]
    });
  }
 
  ngOnInit(): void {
    
    this.checkUser()

  }
role:string;
checkUser(){
  this.role = localStorage.getItem('userRole');
  console.log(this.role);

  if(this.role!="Admin"){
   console.log(localStorage.getItem["userRole"])
   this.logout()
  }
}
 logout(){
    this.authService.logout();
    this.router.navigate(['/login'])
  }
  addPhysicalTraining(): void {
    console.log(this.isIndoor)
    if(this.isIndoor=="true"){
      this.physicalTrainingForm.value.IsIndoor=true;
    }
    else{
      this.physicalTrainingForm.value.IsIndoor=false;
    }
    this.formSubmitted = true;
    if (this.physicalTrainingForm.valid) {
      this.pts.addPhysicalTraining(this.physicalTrainingForm.value).subscribe(() => {
        this.router.navigate(['/viewTraining']);
      });
    }
  }
 
  isFieldInvalid(fieldName: string): boolean {
    const field = this.physicalTrainingForm.get(fieldName);
    return field?.invalid && this.formSubmitted;
  }
}