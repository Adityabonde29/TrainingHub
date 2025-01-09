import { Component, OnInit } from '@angular/core';
import { PhysicalTrainingRequest } from 'src/app/models/physical-training-request.model';
import { PhysicalTrainingService } from 'src/app/services/physical-training.service';
import { UserService } from 'src/app/services/user.service';
import { UserDto } from 'src/app/models/user.dto';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-adminviewappliedrequest',
  templateUrl: './adminviewappliedrequest.component.html',
  styleUrls: ['./adminviewappliedrequest.component.css']
})
export class AdminviewappliedrequestComponent implements OnInit {
  trainingRequests: PhysicalTrainingRequest[] = [];
  filteredRequests: PhysicalTrainingRequest[] = [];
  searchTerm: string = '';
  statusFilter: string = '';
  trainingNames: { [key: number]: string } = {};
  users: { [key: number]: UserDto } = {};
  selectedUser: UserDto | null = null;
  showModal = false;

  constructor(
    private trainingService: PhysicalTrainingService,
    private userService: UserService,
    private router:Router,
    private authService :AuthService
  ) {}
  
  ngOnInit(): void {
    this.checkUser()
    this.loadRequests();
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


 

  getNameById(id: number): void {
    if (!this.trainingNames[id]) {
      this.trainingService.getPhysicalTrainingById(id.toString()).subscribe(data => {
        this.trainingNames[id] = data.trainingName;
      });
    }
  }

  loadRequests(): void {
    this.trainingService.getAllPhysicalTrainingRequests().subscribe(
      (response) => {
        this.trainingRequests = response;
        this.trainingRequests.forEach(request => {
          this.getNameById(request.physicalTrainingId);
          this.loadUserDetails(request.userId);
        });
        this.filterRequests();
      },
      (error) => console.error('Error fetching requests', error)
    );
  }

  loadUserDetails(userId: number): void {
    if (!this.users[userId]) {
      this.userService.getUserById(userId).subscribe(user => {
        this.users[userId] = user;
      });
    }
  }

  getUserDetails(userId: number): UserDto | undefined {
    return this.users[userId];
  }

  filterRequests(): void {
    if (this.statusFilter === "" && this.searchTerm === "") {
      this.filteredRequests = this.trainingRequests;
    } else {
      this.filteredRequests = this.trainingRequests.filter(request => {
        const matchesSearchTerm = this.trainingNames[request.physicalTrainingId]?.toLowerCase().includes(this.searchTerm.toLowerCase());
        const matchesStatusFilter = this.statusFilter === '' || request.status.toLowerCase() === this.statusFilter.toLowerCase();
        return matchesSearchTerm && matchesStatusFilter;
      });
    }
  }

  approveRequest(request: PhysicalTrainingRequest): void {
    request.status = "approved";
    this.trainingService.updatePhysicalTrainingRequest(request.physicalTrainingRequestId.toString(), request).subscribe(() => this.loadRequests());
  }

  rejectRequest(request: PhysicalTrainingRequest): void {
    request.status = "rejected";
    this.trainingService.updatePhysicalTrainingRequest(request.physicalTrainingRequestId.toString(), request).subscribe(() => this.loadRequests());
  }

  onShowModal(id: number): void {
    this.showModal = true;
    this.selectedUser = this.users[id];
  }

  closeModal(): void {
    this.showModal = false;
  }
}
