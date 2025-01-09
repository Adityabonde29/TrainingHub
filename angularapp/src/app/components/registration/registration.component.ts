import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-register',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;
  formSubmitted: boolean = false;
  errorMessage: string = '';
  showPopup: boolean = false;
  popupMessage: string = '';
  isLoading: boolean = false;
  showRegisteringModal: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
      Username: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
      ]],
      ConfirmPassword: ['', [Validators.required, this.matchPassword('Password')]],
      MobileNumber: ['', [
        Validators.required,
        Validators.pattern(/^\d{10}$/) // Ensures exactly 10 digits
      ]],
      UserRole: ['', Validators.required]
    });
  }

  matchPassword(passwordControlName: string) {
    return (control: FormControl) => {
      const passwordControl = this.registerForm?.get(passwordControlName);
      if (passwordControl && control.value !== passwordControl.value) {
        return { passwordMismatch: true };
      }
      return null;
    };
  }

  async handleRegister() {
    this.formSubmitted = true;
    if (this.registerForm.invalid) {
      this.errorMessage = 'Please fill all the required fields correctly';
      return;
    }
    
    const user: User = this.registerForm.value;
    this.showRegisteringModal = true;

    this.authService.register(user).subscribe(
      (data) => {
        console.log(data);
        this.router.navigate(['/login']);
      },
      (error) => {
        console.log("Something Error occurred: ", error);
        this.errorMessage = 'Registration failed. Please try again.';
        this.showRegisteringModal = false;
      },
      () => {
        console.log("Admin/User Added successfully");
        this.showRegisteringModal = false;
      }
    );
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.registerForm.get(fieldName);
    return field?.invalid && this.formSubmitted;
  }

  checkRole(role: string) {
    if (role === "Admin") {
      this.showPopup = true;
      this.popupMessage = "Please Enter Secret Key:";
    }
  }

  secretKey : string = "";
  validateSecretKey() {
    console.log(this.secretKey);
    this.authService.verifySecretKey(this.secretKey).subscribe(
      (isValid) => {
        if (!isValid) {
          this.popupMessage = "Invalid Secret Key! Redirecting to Home page.";
          setTimeout(() => {
            this.router.navigate(['/home']);
            this.showPopup = false;
          }, 2000);
        } else {
          this.showPopup = false;
        }
      },
      (error) => {
        this.popupMessage = "Invalid Secret Key! Redirecting to Home page.";
        setTimeout(() => {
          this.router.navigate(['/home']);
          this.showPopup = false;
        }, 2000);
      }
    );
  }

  closePopup() {
    this.showPopup = false;
  }
}