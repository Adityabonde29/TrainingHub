import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { PhysicalTrainingService } from 'src/app/services/physical-training.service';

@Component({
  selector: 'app-useraddrequest',
  templateUrl: './useraddrequest.component.html',
  styleUrls: ['./useraddrequest.component.css']

})
export class UseraddrequestComponent implements OnInit {
  requestForm: FormGroup;
  formSubmitted: boolean = false;
  status: string = 'Pending'; // Default status
  physicalTrainingId: number = 0; // Initialize to 0

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private trainingService: PhysicalTrainingService
  ) {
    this.requestForm = this.fb.group({
      healthCondition: ['', Validators.required],
      fitnessGoals: ['', Validators.required],
      comments: ['']
    });
  }

  ngOnInit(): void {
   
    this.route.params.subscribe(params => {
      this.physicalTrainingId = +params['id']; 
    });
  }

  submit(): void {
    this.formSubmitted = true;
    if (this.requestForm.invalid) {
      return;
    }

    // Create the new request object
    const newRequest = {
      physicalTrainingRequestId: 0,
      userId: parseInt(localStorage.getItem('userId') || '0', 10),
      physicalTrainingId: this.physicalTrainingId, // Use the extracted ID
      requestDate: new Date().toISOString(), // Current date
      status: this.status,
      healthConditions: this.requestForm.value.healthCondition,
      fitnessGoals: this.requestForm.value.fitnessGoals,
      comments: this.requestForm.value.comments
    };

    // Call the service to submit the new request
    this.trainingService.addPhysicalTrainingRequest(newRequest).subscribe(
      (response) => {
        alert('Successfully Submitted!');
        // Redirect to userviewappliedrequest component
        this.router.navigate(['/Userviewappliedrequest']);
      },
      (error) => console.error('Error creating request', error)
    );
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.requestForm.get(fieldName);
    return field?.invalid && this.formSubmitted;
  }

  goBack() {
    // Navigate back to userviewappliedrequest component
    this.router.navigate(['/Userviewtraining']);
  }
}