import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserDto } from '../models/user.dto'; // Adjust the path as necessary

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://8080-ceefcdbebfafdbfbabfceefbacfdcadccdcfaff.premiumproject.examly.io/api/User';

  constructor(private http: HttpClient) {}

  getUserById(userId: number): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.apiUrl}/${userId}`);
  }

  updateUser(userId:number,user: UserDto):Observable<UserDto>{
    return this.http.put<UserDto>(`${this.apiUrl}/${userId}`,user)
  }
}
