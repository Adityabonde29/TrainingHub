import { Component, OnInit } from '@angular/core';
import { PhysicalTraining } from 'src/app/models/physical-training.model';
import { PhysicalTrainingService } from 'src/app/services/physical-training.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-adminviewtraining',
  templateUrl: './adminviewtraining.component.html',
  styleUrls: ['./adminviewtraining.component.css']
})
export class AdminviewtrainingComponent implements OnInit {
  trainings: PhysicalTraining[] = [];
  filteredTrainings: PhysicalTraining[] = [];
  searchTerm: string = '';
  showDeleteModal = false;
  selectedTrainingId: string;

  constructor(private trainingService: PhysicalTrainingService, private router: Router, private authService:AuthService) {}
  
  ngOnInit(): void {
    this.checkUser()
    this.loadTrainings();
  }
  role:string;
checkUser(){
  this.role = localStorage.getItem('userRole');
  console.log(this.role);

  if(this.role!="Admin"){
   console.log(localStorage.getItem["userRole"])
   this.authService.logout();
   this.router.navigate(['/login'])
  }
}

  loadTrainings(): void {
    this.trainingService.getAllPhysicalTrainings().subscribe((data) => {
      this.trainings = data;
      this.filteredTrainings = data;
      console.log(this.trainings[0].trainingName)
    });
    

  }

  searchTraining(): void {
    
    
    if(this.searchTerm==""){
      this.loadTrainings()
    }
    else {
      const lowerCaseSearchTerm = this.searchTerm.toLowerCase();
      this.filteredTrainings = this.trainings.filter(training =>
        training.trainingName.toLowerCase().includes(lowerCaseSearchTerm) ||
        training.description.toLowerCase().includes(lowerCaseSearchTerm) ||
        training.trainerName.toLowerCase().includes(lowerCaseSearchTerm) ||
        training.location.toLowerCase().includes(lowerCaseSearchTerm) ||
        training.isIndoor.toString().toLowerCase().includes(lowerCaseSearchTerm) ||
        training.fee.toString().toLowerCase().includes(lowerCaseSearchTerm) ||
        training.focusArea.toLowerCase().includes(lowerCaseSearchTerm) ||
        training.physicalRequirements.toLowerCase().includes(lowerCaseSearchTerm)
      );
    }


  
  }

  editTraining(trainingId: number): void {
    this.router.navigate([`/editTraining/${trainingId}`]);
  }

  confirmDelete(trainingId: string): void {
    this.selectedTrainingId = trainingId;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
  }

  deleteTraining(): void {
   
    this.trainingService.deletePhysicalTraining(this.selectedTrainingId).subscribe(
      () => {
        this.loadTrainings();
        this.closeDeleteModal();
        alert('Training deleted successfully.');
      },
      (error) => {
        if (error.status === 400) {
          alert("This training can't be delete since it's been request by a User");
        } else {
          console.error('Error deleting request', error);
        }
        this.closeDeleteModal();
      }
    );
  }
}
