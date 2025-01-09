import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-adminnav',
  templateUrl: './adminnav.component.html',
  styleUrls: ['./adminnav.component.css']
})
export class AdminnavComponent implements OnInit {

  constructor(private authService : AuthService, private r : Router) { }

  ngOnInit(): void {
  //   var role:string= localStorage.getItem["userRole"]
  //   console.log(role)
  //  if(role!="Admin"){
  //   this.authService.logout();
  //   this.r.navigate(['/login'])
  //  }
  }

  redirecttopage(value : number) : void {
    if(value == 1){
      this.r.navigate(['/addTraining'])
    } else if(value == 2){
      this.r.navigate(['/viewTraining'])
    }
  }

  Admin : String = localStorage.getItem('userName');

  logout(){
    this.authService.logout();
    this.r.navigate(['/home'])
  }

}
