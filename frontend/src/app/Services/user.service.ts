import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
// loginUrl="https://localhost:7126/api/Users/Login"
loginUrl="https://localhost:7126/api/Admins/Login"
AdminSignUpUrl="https://localhost:7126/api/Admins/SignUp"

signUpUrl="https://localhost:7126/api/Users"

  constructor(private http:HttpClient) { }
  loggedIn= new BehaviorSubject<boolean>(false);

  Userlogin(param:any){
    // const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }), responseType:'json' };
    return this.http.post(this.loginUrl,param, {responseType: 'text'})
  }

  UserSignUp(param:any){
    return this.http.post(this.signUpUrl,param, {responseType: 'json'})
  }
    AdminSignUp(param:any, auth_token){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      // Authorization: `Bearer ${auth_token}`,
      Authorization: auth_token,
    });
    const requestOptions = { headers: headers }
    return this.http.post(this.AdminSignUpUrl,param, requestOptions)
  }

  getAllUsers(auth_token: any){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      // Authorization: `Bearer ${auth_token}`,
      Authorization: auth_token,
    });
    const requestOptions = { headers: headers }
    return this.http.get(this.signUpUrl, requestOptions)
  }

  getValue(){
    return this.loggedIn.asObservable()
  }
}
