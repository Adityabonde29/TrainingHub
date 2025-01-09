import { Component, OnInit } from '@angular/core';
import { PhysicalTrainingService } from 'src/app/services/physical-training.service';
import { PhysicalTrainingRequest } from 'src/app/models/physical-training-request.model';
import { PhysicalTraining } from 'src/app/models/physical-training.model';
@Component({
  selector: 'app-userviewappliedrequest',
  templateUrl: './userviewappliedrequest.component.html',
  styleUrls: ['./userviewappliedrequest.component.css']
})
export class UserviewappliedrequestComponent implements OnInit {
  requests: PhysicalTrainingRequest[] = [];
  searchTerm: string = '';
  showDeleteModal: boolean = false;
  requestToDelete: number | null = null;
  userId: number;

  constructor(private trainingService: PhysicalTrainingService) {}

  ngOnInit(): void {
    this.userId = +localStorage.getItem('userId')!;
    this.loadRequests();
    this.userId=+localStorage.getItem('userId');

    // this.physicalTraining=this.trainingService.getPhysicalTrainingById(this.filterRequests[].PhysicalTrainingId)
  }

  loadRequests(): void {
    this.trainingService.getAllPhysicalTrainingRequests().subscribe(
      (response) => {
        console.log(response)
        this.requests = response.filter(request => request.userId === this.userId);
        console.log("Req= ",response)
        this.requests.forEach(request => {
          this.getNameById(request.physicalTrainingId);
        });
      },
      (error) => console.error('Error fetching requests', error)
    );
   
    
  }

  // filterbyUser(): void {
  //   console.log(this.requests);
  //   this.requests=this.requests.filter(data=>data.userId==this.userId);
  //   console.log(this.requests);
  // }
  trainingNames: { [key: number]: string } = {};

  getNameById(id: number): void {
   
    if (!this.trainingNames[id]) {
      this.trainingService.getPhysicalTrainingById(id.toString()).subscribe(data => {
        this.trainingNames[id] = data.trainingName;
      });
    }
  }         


  filterRequests(): void {
    if (this.searchTerm === "") {
      this.loadRequests();
    } else {
      const searchTermLower = this.searchTerm.toLowerCase();
      this.requests = this.requests.filter(request =>
        (request.physicalTrainingRequestId?.toString().toLowerCase().includes(searchTermLower) || '') ||
        request.requestDate.toLowerCase().includes(searchTermLower) ||
        request.status.toLowerCase().includes(searchTermLower) ||
        request.healthConditions.toLowerCase().includes(searchTermLower) ||
        request.fitnessGoals.toLowerCase().includes(searchTermLower) ||
        (request.comments?.toLowerCase().includes(searchTermLower) || '')||
        this.trainingNames[request.physicalTrainingId]?.toLowerCase().includes(searchTermLower)

      );
    }
  }
  

  


  confirmDelete(requestId: number): void {
    this.requestToDelete = requestId;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.requestToDelete = null;
  }

  deleteTraining(): void {
    if (this.requestToDelete !== null) {
      console.log(this.requestToDelete)
      this.trainingService.deletePhysicalTrainingRequest(this.requestToDelete.toString()).subscribe(
        () => {
          this.loadRequests();
          this.closeDeleteModal();
        },
        (error) => console.error('Error deleting request', error)
      );
    }
  }
}
