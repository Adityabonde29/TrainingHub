import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private authService : AuthService) { }

  ngOnInit(): void {

    if(localStorage.getItem('userRole') == 'Admin' || localStorage.getItem('userRole') == 'User'){
      const loader = document.querySelector("#loader") as HTMLElement;
      loader.style.top = "-100%";
    }

    setTimeout(() => {
      const loader = document.querySelector("#loader") as HTMLElement;
      if (loader) {
        loader.style.top = "-100%";
      }
    }, 3000);
  }

  isLoggedIn():boolean
  {
    return this.authService.isLoggedIn();
  }
  isAdmin():boolean
  {
    return this.authService.isAdmin();
  }
  isUser():boolean
  {
    return this.authService.isUser();
  }
}
