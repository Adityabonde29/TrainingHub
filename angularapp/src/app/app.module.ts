import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AdminaddtrainingComponent } from './components/adminaddtraining/adminaddtraining.component';
import { AdminedittrainingComponent } from './components/adminedittraining/adminedittraining.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { UserviewappliedrequestComponent } from './components/userviewappliedrequest/userviewappliedrequest.component';
import { UserviewtrainingComponent } from './components/userviewtraining/userviewtraining.component';
import { UserviewfeedbackComponent } from './components/userviewfeedback/userviewfeedback.component';
import { UseraddfeedbackComponent } from './components/useraddfeedback/useraddfeedback.component';
import { AdminnavComponent } from './components/adminnav/adminnav.component';
import { AdminviewappliedrequestComponent } from './components/adminviewappliedrequest/adminviewappliedrequest.component';
import { AdminviewfeedbackComponent } from './components/adminviewfeedback/adminviewfeedback.component';
import { AdminviewtrainingComponent } from './components/adminviewtraining/adminviewtraining.component';
import { ErrorComponent } from './components/error/error.component';
import { HomeComponent } from './components/home/home.component';
import { UseraddrequestComponent } from './components/useraddrequest/useraddrequest.component';
import { UsernavComponent } from './components/usernav/usernav.component';
import { FooterComponent } from './components/footer/footer.component';
import { AuthInterceptor } from './services/auth.interceptor';
import { AuthGuard } from './components/authguard/auth.guard';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminaddtrainingComponent,
    AdminedittrainingComponent,
    NavbarComponent,
    LoginComponent,
    RegistrationComponent,
    UserviewappliedrequestComponent,
    UserviewtrainingComponent,
    UserviewfeedbackComponent,
    UseraddfeedbackComponent,
    AdminnavComponent,
    AdminviewappliedrequestComponent,
    AdminviewfeedbackComponent,
    AdminviewtrainingComponent,
    ErrorComponent,
    HomeComponent,
    UseraddrequestComponent,
    UsernavComponent,
    FooterComponent,
    UserProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
