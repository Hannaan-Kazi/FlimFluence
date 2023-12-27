// auth.service.ts

import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { NavigationEnd } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  currentUrl: string;
  alag: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor(private jwtHelper: JwtHelperService, private router: Router) {}
  loggedIn = new BehaviorSubject<boolean>(this.isAdmin());

  isAuthenticated(): boolean {
    const token = localStorage.getItem('AuthJwtToken');
    return token && !this.jwtHelper.isTokenExpired(token);
  }
  
  bahotAlag() {
    return this.alag;
  }

  isAdmin(): boolean {
    const token = localStorage.getItem('AuthJwtToken');
    const decodedToken = this.jwtHelper.decodeToken(token);
    let isAdmin = decodedToken && decodedToken.role === 'admin' ? true: false
    console.log("admin hai?",isAdmin);
    
    this.alag.next(isAdmin);
    return isAdmin;
    // return decodedToken && decodedToken.role === 'admin' ? true : false; // Adjust based on your JWT structure
  }

  refresh() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.currentUrl = this.router.url;
        this.router.navigate([this.currentUrl]);
      }
    });
  }
}
