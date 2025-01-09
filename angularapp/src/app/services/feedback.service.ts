import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feedback } from '../models/feedback.model';
import { User } from '../models/user.model';
 
@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  
  private baseUrl = 'https://8080-ceefcdbebfafdbfbabfceefbacfdcadccdcfaff.premiumproject.examly.io/api/feedback';

  constructor(private http: HttpClient) { }

  
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }
 
  sendFeedback(feedback: Feedback): Observable<Feedback> {
    return this.http.post<Feedback>(`${this.baseUrl}`, feedback, {
      headers: this.getAuthHeaders()
    });
  }
 
  getAllFeedbacksByUserId(userId: number): Observable<Feedback[]> {
    console.log(userId, `${this.baseUrl}/user/${userId}`)
    
    return this.http.get<Feedback[]>(`${this.baseUrl}/user/${userId}`, {
      headers: this.getAuthHeaders()
    });
  }
 
  deleteFeedback(feedbackId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${feedbackId}`, {
      headers: this.getAuthHeaders()
    });
  }
 
  getFeedbacks(): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(`${this.baseUrl}`, {
      headers: this.getAuthHeaders()
    });
  }

  getUser(id : number) : Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/`);
  }
}