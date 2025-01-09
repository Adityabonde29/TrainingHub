import { Component, OnInit } from '@angular/core';
import { PhysicalTrainingService } from 'src/app/services/physical-training.service';
import { PhysicalTraining } from 'src/app/models/physical-training.model';
import { Router } from '@angular/router';
import { PhysicalTrainingRequest } from 'src/app/models/physical-training-request.model';
 
@Component({
  selector: 'app-userviewtraining',
  templateUrl: './userviewtraining.component.html',
  styleUrls: ['./userviewtraining.component.css']
})
export class UserviewtrainingComponent implements OnInit {
  trainings: PhysicalTraining[] = [];
  filteredTrainings: PhysicalTraining[] = [];
  searchTerm: string = '';
  appliedBtn: { [key: number]: boolean } = {};
 
  constructor(private trainingService: PhysicalTrainingService, private router: Router, private physicalRequest: PhysicalTrainingService) {}
 
  ngOnInit(): void {
    this.loadTrainings();
  }
 
  loadTrainings(): void {
    this.trainingService.getAllPhysicalTrainings().subscribe(
      (response) => {
        this.trainings = response;
        this.filteredTrainings = response; // Initialize filteredTrainings
        response.forEach(element => {
          this.isApplied(element.physicalTrainingId);
        });
      },
      (error) => console.error('Error fetching trainings', error)
    );
  }
 
  applyForTraining(trainingId: string): void {
    this.router.navigate([`/Useraddrequest/${trainingId}`]);
  }
 
  searchTraining(): void {
    const searchTermLower = this.searchTerm.toLowerCase();
    this.filteredTrainings = this.trainings.filter(training =>
      training.trainingName.toLowerCase().includes(searchTermLower) ||
      training.description.toLowerCase().includes(searchTermLower) ||
      training.trainerName.toLowerCase().includes(searchTermLower) ||
      training.location.toLowerCase().includes(searchTermLower) ||
      training.isIndoor.toString().toLowerCase().includes(searchTermLower) ||
      training.fee.toString().toLowerCase().includes(searchTermLower) ||
      training.focusArea.toLowerCase().includes(searchTermLower) ||
      training.physicalRequirements.toLowerCase().includes(searchTermLower)
    );
  }
 
  isApplied(id: number): void {
    this.physicalRequest.getAllPhysicalTrainingRequests().subscribe(
      (response) => {
        const userId = +localStorage.getItem('userId');
        const userRequests = response.filter(request => request.userId === userId);
 
        this.appliedBtn[id] = userRequests.some(request => request.physicalTrainingId === id);
      },
      (error) => {
        console.error('Error loading training requests:', error);
        this.appliedBtn[id] = false;
      }
    );
  }
}