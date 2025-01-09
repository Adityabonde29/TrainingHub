import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';
import { Login } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
 
  public apiUrl:string = "https://8080-ceefcdbebfafdbfbabfceefbacfdcadccdcfaff.premiumproject.examly.io";
 
  constructor(private http:HttpClient) { }

  verifySecretKey(secretKey: string): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/api/verify-secret-key`, { secretKey });
  }

  register(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/api/register`, user);
  }

  login(login: Login): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/login`, login);
  }

  isRole(): void {
    const token = localStorage.getItem("Token");
    if (!token) {
      console.error('Token not found in local storage');
      return;
    }

    const tokenParts = token.split('.');
    if (tokenParts.length !== 3) {
      console.error('Invalid token format');
      return;
    }

    let payload;
    try {
      payload = JSON.parse(atob(tokenParts[1]));
    } catch (error) {
      console.error('Error decoding token payload', error);
      return;
    }

    const userRole = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    const userName = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    const userId = payload['UserID'];

    if (!userRole || !userName || !userId) {
      console.error('Required fields not found in token payload');
      return;
    }

    localStorage.setItem('userRole', userRole);
    localStorage.setItem('userName', userName);
    localStorage.setItem('userId', userId);
  }

  isRoles(): any {
    const token = localStorage.getItem("Token");
    if (!token) {
      console.error('Token not found in local storage');
      return null;
    }

    const tokenParts = token.split('.');
    if (tokenParts.length !== 3) {
      console.error('Invalid token format');
      return null;
    }

    let payload;
    try {
      payload = JSON.parse(atob(tokenParts[1]));
    } catch (error) {
      console.error('Error decoding token payload', error);
      return null;
    }

    localStorage.setItem('userRole', payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
    localStorage.setItem('userName', payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);
    localStorage.setItem('userId', payload['UserID']);
    return payload;
  }

  isLoggedIn(): boolean {
    const userRole = localStorage.getItem('userRole');
    return userRole === "Admin" || userRole === "User";
  }

  isAdmin(): boolean {
    return localStorage.getItem('userRole') === "Admin";
  }

  isUser(): boolean {
    return localStorage.getItem('userRole') === "User";
  }

  logout(): void {
    localStorage.removeItem('userRole');
    localStorage.removeItem('userName');
    localStorage.removeItem('userId');
    localStorage.removeItem('Token');
  }

  decodeToken(): any {
    const token = localStorage.getItem("Token");
    if (!token) {
      console.error('Token not found in local storage');
      return null;
    }

    const tokenParts = token.split('.');
    if (tokenParts.length !== 3) {
      console.error('Invalid token format');
      return null;
    }

    try {
      return JSON.parse(atob(tokenParts[1]));
    } catch (error) {
      console.error('Error decoding token payload', error);
      return null;
    }
  }

  setRoleData(): void {
    const payload = this.decodeToken();
    if (payload) {
      localStorage.setItem('userRole', payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
      localStorage.setItem('userName', payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);
      localStorage.setItem('userId', payload['UserID']);
    }
  }
}
