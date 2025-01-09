import { Component, OnInit } from '@angular/core';
import { FeedbackService } from 'src/app/services/feedback.service';
import { Feedback } from 'src/app/models/feedback.model';
@Component({
  selector: 'app-userviewfeedback',
  templateUrl: './userviewfeedback.component.html',
  styleUrls: ['./userviewfeedback.component.css']
})
export class UserviewfeedbackComponent implements OnInit {
  feedbacks: Feedback[];
  showDeleteModal: boolean = false;
  feedbackToDelete: number | null = null;

  constructor(private feedbackService: FeedbackService) { }

  ngOnInit(): void {
    this.loadFeedbacks();
  }

  loadFeedbacks(): void {
    console.log("feedback loaded");
    const userId = +localStorage.getItem('userId');
    console.log("Userid",userId);
    this.feedbackService.getAllFeedbacksByUserId(userId).subscribe((response) => {
      this.feedbacks = response;
      console.log(this.feedbacks);
    }
    );
    console.log("outside= ", this.feedbacks)
    
  }
  confirmDelete(feedbackId: number): void {
    this.feedbackToDelete = feedbackId;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.feedbackToDelete = null;
  }

  deleteFeedback(): void {
    if (this.feedbackToDelete !== null) {
      this.feedbackService.deleteFeedback(this.feedbackToDelete).subscribe(
        (response) => {
          this.loadFeedbacks();
          this.closeDeleteModal();
        },
        (error) => console.error('Error deleting feedback', error)
      );
    }
  }
}
