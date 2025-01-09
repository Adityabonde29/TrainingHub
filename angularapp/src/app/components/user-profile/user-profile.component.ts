import { Component, OnInit } from '@angular/core';
import { UserDto } from 'src/app/models/user.dto';
import { UserService } from 'src/app/services/user.service';



@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  userProfile: UserDto
  
  
  
  userId:number
  userRole:string;
  isEditModalOpen = false;

  constructor(private userService:UserService) { }

  ngOnInit(): void {
   
    this.loadUserDetails();
    
  }

  loadUserDetails(): void {

    this.userId=+localStorage.getItem('userId')
    console.log(this.userId)
    this.userRole=localStorage.getItem('userRole');
      this.userService.getUserById(this.userId).subscribe(user => {
        console.log(this.userProfile);
        this.userProfile=user
        console.log(this.userProfile);
      });
    
  }

  openEditModal(): void {
    console.log(this.userProfile);
    this.isEditModalOpen = true;
  }

  closeEditModal(): void {
    this.isEditModalOpen = false;
  }

  saveChanges(): void {

    this.userService.updateUser(this.userId,this.userProfile).subscribe(data=>{
      console.log(data);
      this.loadUserDetails()
    }
      

    )
    console.log('Profile updated:', this.userProfile);

    this.closeEditModal();
  }
}
