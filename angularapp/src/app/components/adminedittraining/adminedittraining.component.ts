import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PhysicalTrainingService } from 'src/app/services/physical-training.service';
import { PhysicalTraining } from 'src/app/models/physical-training.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-adminedittraining',
  templateUrl: './adminedittraining.component.html',
  styleUrls: ['./adminedittraining.component.css']
})
export class AdminedittrainingComponent implements OnInit {
  physicalTrainingForm: FormGroup;
  formSubmitted: boolean = false;
  trainingId: string;
  isIndoor: string = "false";
  showSuccessMessage: boolean = false; // Add this line

  constructor(
    private fb: FormBuilder,
    private pts: PhysicalTrainingService,
    private router: Router,
    private route: ActivatedRoute,
    private authService:AuthService
  ) {
    this.physicalTrainingForm = this.fb.group({
      trainingName: ['', Validators.required],
      description: ['', Validators.required],
      trainerName: ['', Validators.required],
      location: ['', Validators.required],
      isIndoor: [true, Validators.required],
      fee: [0, [Validators.required, Validators.min(0)]],
      focusArea: ['', Validators.required],
      physicalRequirements: ['', Validators.required]
    });
  }
 
  ngOnInit(): void {
    this.checkUser()
    this.route.params.subscribe(params => {
      this.trainingId = params['id'];
      this.loadTrainingData();

    });
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
 
 
  loadTrainingData(): void {
    this.pts.getPhysicalTrainingById(this.trainingId).subscribe(training => {
      console.log(training);
      this.physicalTrainingForm.patchValue(training);
    });
  }
 
  updatePhysicalTraining(): void {
    this.formSubmitted = true;
    if (this.isIndoor == "true") {
      this.physicalTrainingForm.value.isIndoor = true;
    } else {
      this.physicalTrainingForm.value.isIndoor = false;
    }
    console.log(this.physicalTrainingForm.value);
    if (this.physicalTrainingForm.valid) {
      this.pts.updatePhysicalTraining(this.trainingId, this.physicalTrainingForm.value).subscribe(() => {
        this.showSuccessMessage = true; // Show success message
      });
    }
  }
 
  isFieldInvalid(fieldName: string): boolean {
    const field = this.physicalTrainingForm.get(fieldName);
    return field?.invalid && this.formSubmitted;
  }
 
  goBack(): void {
    this.router.navigate(['/viewTraining']);
  }

  closeModal(): void {
    this.showSuccessMessage = false;
    this.router.navigate(['/viewTraining']);
  }
}