import { Component, OnInit } from '@angular/core';
import { FeedbackService } from 'src/app/services/feedback.service';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { Feedback } from 'src/app/models/feedback.model';
import { User } from 'src/app/models/user.model';
import { UserDto } from 'src/app/models/user.dto';
import { UserService } from 'src/app/services/user.service';
 
@Component({
  selector: 'app-adminviewfeedback',
  templateUrl: './adminviewfeedback.component.html',
  styleUrls: ['./adminviewfeedback.component.css']
})
export class AdminviewfeedbackComponent implements OnInit {
  feedbacks: Feedback[] = [];
  selectedUser: UserDto | null = null;

  showModal = false;
  showLogoutModal = false;
  users: { [key: number]: UserDto } = {}; 

 
  constructor(
    private feedbackService: FeedbackService,
    private authService: AuthService,
    private router: Router,
    private userService: UserService
  ) {}
  


  ngOnInit(): void {
    this.checkUser()
    this.loadFeedbacks();
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
 
  loadFeedbacks(): void {
    this.feedbackService.getFeedbacks().subscribe(feedbacks => {
      this.feedbacks = feedbacks;
      this.feedbacks.forEach(feedback => {
        this.loadUserDetails(feedback.userId);
        console.log(this.loadUserDetails)
      });
    });
  }

  loadUserDetails(userId: number): void {
    if (!this.users[userId]) {
      this.userService.getUserById(userId).subscribe(user => {
        this.users[userId] = user;
        console.log(this.users)
      });
    }
  }

  getUserDetails(userId: number): UserDto | undefined {
    return this.users[userId];
  }


 
  // showProfile(userId: number): void {
  //   this.userService.getUserById(userId).subscribe((user: User) => {
  //     this.selectedUser = user;
  //     this.showModal = true;
  //   });
  // }
  onShowModal(id:number):void{
    this.showModal=true;
    this.selectedUser=this.users[id]
    console.log(this.selectedUser);
  }
  
  closeModal(): void {
    this.showModal = false;
  }
 
  showLogoutConfirmation(): void {
    this.showLogoutModal = true;
  }
 
  closeLogoutModal(): void {
    this.showLogoutModal = false;
  }
 
  confirmLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}