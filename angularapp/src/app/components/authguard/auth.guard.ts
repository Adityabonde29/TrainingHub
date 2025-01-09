import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const currentLink: string = state.url;

    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return false;
    }

    const userRole = localStorage.getItem('userRole');

    if (userRole === 'Admin' && currentLink.includes('user')) {
      this.authService.logout();
      this.router.navigate(['/login']);
      return false;
    } else if (userRole === 'User' && currentLink.includes('admin')) {
      this.authService.logout();
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}
