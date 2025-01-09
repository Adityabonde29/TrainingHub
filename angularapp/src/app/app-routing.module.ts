import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AdminaddtrainingComponent } from './components/adminaddtraining/adminaddtraining.component';
import { AdminedittrainingComponent } from './components/adminedittraining/adminedittraining.component';
import { AdminviewfeedbackComponent } from './components/adminviewfeedback/adminviewfeedback.component';
import { AdminviewappliedrequestComponent } from './components/adminviewappliedrequest/adminviewappliedrequest.component';
import { AdminviewtrainingComponent } from './components/adminviewtraining/adminviewtraining.component';
import { UseraddfeedbackComponent } from './components/useraddfeedback/useraddfeedback.component';
import { UseraddrequestComponent } from './components/useraddrequest/useraddrequest.component';
import { UserviewappliedrequestComponent } from './components/userviewappliedrequest/userviewappliedrequest.component';
import { UserviewfeedbackComponent } from './components/userviewfeedback/userviewfeedback.component';
import { UserviewtrainingComponent } from './components/userviewtraining/userviewtraining.component';
import { AuthGuard } from './components/authguard/auth.guard';
import { AuthInterceptor } from './services/auth.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'addTraining', component: AdminaddtrainingComponent, canActivate: [AuthGuard] },
  { path: 'editTraining/:id', component: AdminedittrainingComponent, canActivate: [AuthGuard] },
  { path: 'viewFeedback', component: AdminviewfeedbackComponent, canActivate: [AuthGuard] },
  { path: 'viewAppliedRequest', component: AdminviewappliedrequestComponent, canActivate: [AuthGuard] },
  { path: 'viewTraining', component: AdminviewtrainingComponent, canActivate: [AuthGuard] },
  { path: 'Useraddfeedback', component: UseraddfeedbackComponent, canActivate: [AuthGuard] },
  { path: 'Useraddrequest/:id', component: UseraddrequestComponent, canActivate: [AuthGuard] },
  { path: 'Userviewappliedrequest', component: UserviewappliedrequestComponent, canActivate: [AuthGuard] },
  { path: 'Userviewfeedback', component: UserviewfeedbackComponent, canActivate: [AuthGuard] },
  { path: 'Userviewtraining', component: UserviewtrainingComponent, canActivate: [AuthGuard] },
  {path: 'userProfile', component:UserProfileComponent},
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
})
export class AppRoutingModule { }