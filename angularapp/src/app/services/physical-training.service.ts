import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PhysicalTraining } from '../models/physical-training.model';
import { PhysicalTrainingRequest } from '../models/physical-training-request.model';
 
@Injectable({
  providedIn: 'root'
})
export class PhysicalTrainingService {
  deleteTraining(selectedTrainingId: number) {
    throw new Error('Method not implemented.');
  }

  private baseUrl = 'https://8080-ceefcdbebfafdbfbabfceefbacfdcadccdcfaff.premiumproject.examly.io/api';
  
  constructor(private http: HttpClient) { }
 
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }
 
  getAllPhysicalTrainings(): Observable<PhysicalTraining[]> {
    return this.http.get<PhysicalTraining[]>(`${this.baseUrl}/physicalTraining`, {
      headers: this.getAuthHeaders()
    });
  }
 
  getPhysicalTrainingById(trainingId: string): Observable<PhysicalTraining> {
    return this.http.get<PhysicalTraining>(`${this.baseUrl}/physicalTraining/${trainingId}`, {
      headers: this.getAuthHeaders()
    });
  }
 
  addPhysicalTraining(training: PhysicalTraining): Observable<PhysicalTraining> {
    console.log(training)
    return this.http.post<PhysicalTraining>(`${this.baseUrl}/physicalTraining`, training, {
      headers: this.getAuthHeaders()
    });
  }
 
  updatePhysicalTraining(trainingId: string, training: PhysicalTraining): Observable<PhysicalTraining> {
    return this.http.put<PhysicalTraining>(`${this.baseUrl}/physicalTraining/${trainingId}`, training, {
      headers: this.getAuthHeaders()
    });
  }
 
  deletePhysicalTraining(trainingId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/physicalTraining/${trainingId}`, {
      headers: this.getAuthHeaders()
    });
  }
 
  getAllPhysicalTrainingRequests(): Observable<PhysicalTrainingRequest[]> {
    return this.http.get<PhysicalTrainingRequest[]>(`${this.baseUrl}/physicaltrainingrequest`, {
      headers: this.getAuthHeaders()
    });
  }
 
  getPhysicalTrainingRequestsByUserId(userId: string): Observable<PhysicalTrainingRequest[]> {
    return this.http.get<PhysicalTrainingRequest[]>(`${this.baseUrl}/physicaltrainingrequest/user/${userId}`, {
      headers: this.getAuthHeaders()
    });
  }
 
  addPhysicalTrainingRequest(request: PhysicalTrainingRequest): Observable<PhysicalTrainingRequest> {
    return this.http.post<PhysicalTrainingRequest>(`${this.baseUrl}/physicaltrainingrequest`, request, {
      headers: this.getAuthHeaders()
    });
  }
 
  updatePhysicalTrainingRequest(requestId: string, request: PhysicalTrainingRequest): Observable<PhysicalTrainingRequest> {
    return this.http.put<PhysicalTrainingRequest>(`${this.baseUrl}/physicaltrainingrequest/${requestId}`, request, {
      headers: this.getAuthHeaders()
    });
  }
 
  deletePhysicalTrainingRequest(requestId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/physicaltrainingrequest/${requestId}`, {
      headers: this.getAuthHeaders()
    });
  }
}