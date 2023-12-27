import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserService } from './user.service';
import { Ratings } from '../models/ratings-Model';

@Injectable({
  providedIn: 'root'
})
export class RatingsService {
  ratingUrl='https://localhost:7126/api/Ratings'


  constructor(private http:HttpClient,private userService:UserService) {

   }

   
    token= localStorage.getItem('AuthJwtToken')
   

   postRating(rating:Ratings){
    const headers= new HttpHeaders({'Content-Type':'application/json',Authorization:this.token})
    const requestOptions={headers:headers}
    return this.http.post(this.ratingUrl, rating, requestOptions)
   }

}
