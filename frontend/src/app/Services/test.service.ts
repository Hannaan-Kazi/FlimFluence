import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Movies } from '../models/movies-Model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  private apiUrl = 'https://localhost:7126/api/TestImage';
  
  private moviePostUrl = 'https://localhost:7126/api/Movies';
  private moviePostUrltest = 'https://localhost:7126/api/Movies/test';
  constructor(private http: HttpClient) { }

  getImage(){
    return this.http.get<any>(this.apiUrl);
  }

  getImageFromMovieTable(id:number):Observable<Movies>{
    return this.http.get<Movies>(`${this.moviePostUrl}/${id}`)
  }

  uploadImage(imageData: any) {
    // const headers= new HttpHeaders({'Content-Type':'form-data'})
    // const requestOptions={headers:headers}
    // return this.http.post<any>(this.apiUrl, imageData, requestOptions);
    const headers= new HttpHeaders({'Content-Type':'application/json; charset=utf-8',Authorization: localStorage.getItem('AuthJwtToken')})
    const requestOptions={headers:headers}
    return this.http.post<any>(this.apiUrl,imageData, requestOptions);
  }

  addMovie(imageData: any){
    const headers= new HttpHeaders({Authorization: localStorage.getItem('AuthJwtToken')})
    const requestOptions={headers:headers}
    return this.http.post<any>(this.moviePostUrl, imageData, requestOptions );
  }

}
