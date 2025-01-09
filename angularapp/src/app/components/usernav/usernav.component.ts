import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-usernav',
  templateUrl: './usernav.component.html',
  styleUrls: ['./usernav.component.css']
})
export class UsernavComponent implements OnInit {

  constructor(private authService : AuthService, private r : Router){ }

  ngOnInit(): void {
  }

  User : string = localStorage.getItem('userName');

  logout(){
    this.authService.logout();
    this.r.navigate(['/home'])
  }

  redirecttopage(value : number) : void {
    if(value == 1){
      this.r.navigate(['/Useraddfeedback'])
    } else if(value == 2){
      this.r.navigate(['/Userviewfeedback'])
    }
  }

}
