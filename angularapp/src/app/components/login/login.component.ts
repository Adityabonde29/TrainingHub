import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Login } from '../../models/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage: string = '';
  passwordFieldType: string = 'password';
  role: string = '';
  showPopup: boolean = false;
  popupMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  handleLogin() {
    if (this.loginForm.invalid) {
      this.errorMessage = 'Please fill all the required fields correctly';
      return;
    }

    const loginData: Login = this.loginForm.value;
    console.log(loginData);
    this.isLoading = true;

    this.authService.login(loginData).subscribe((res) => {
      console.log(res);
      localStorage.setItem("Token", res.token);
      this.authService.setRoleData(); 
      console.log("component= ", localStorage);
      this.role = localStorage.getItem('userRole');
      console.log(this.role);

      console.log(localStorage.getItem('userId'));

      if (this.role === "Admin" || this.role === "User") {
        this.router.navigate(['/home']);
      }
    }, (error) => {
      this.showPopup = true;
      this.isLoading = false;
      if (error.status == '401') {
        this.popupMessage = "User doesn't exist";
      } else {
        this.popupMessage = "Invalid Credentials";
      }
      this.errorMessage = 'Login failed. Please check your credentials and try again.';
      console.error('Login error:', error);
    });
  }

  togglePasswordVisibility() {
    this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password';
  }

  closePopup() {
    this.showPopup = false;
  }
}