import { Component } from '@angular/core';
import { FeedbackService } from 'src/app/services/feedback.service';
import { Feedback } from 'src/app/models/feedback.model';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
 
@Component({
  selector: 'app-useraddfeedback',
  templateUrl: './useraddfeedback.component.html',
  styleUrls: ['./useraddfeedback.component.css']
})
export class UseraddfeedbackComponent {

  feedbackText: string = '';
  showSuccessMessage: boolean = false;
 
  constructor(private feedbackService: FeedbackService , private authService: AuthService, private r : Router) {}
 
  onSubmit() {
    if (!this.feedbackText) {
      return;
    }
 
    const feedback: Feedback = {
      userId: +localStorage.getItem('userId'),
      feedbackText: this.feedbackText,
      date: new Date(),
    };
    console.log(this.authService.isRoles());
 
    this.feedbackService.sendFeedback(feedback).subscribe(
      (response) => {
        this.showSuccessMessage = true;
      },
      (error) => {
        console.error('Error submitting feedback', error);
      }
    );
  }
 
  closeModal() {
    this.showSuccessMessage = false;
    this.feedbackText = '';
    this.r.navigate(['/Userviewfeedback']);
  }
}
