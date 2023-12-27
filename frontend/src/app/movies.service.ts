import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Movies } from './models/movies-Model';
import { Observable } from 'rxjs';
import { Ratings } from './models/ratings-Model';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {
  url='https://localhost:7126/api/Movies'
  ratingUrl='https://localhost:7126/api/Ratings'

  constructor(private http:HttpClient) { }
  getPosts(): Observable<Movies[]> {
    return this.http.get<Movies[]>(this.url)
  }

  getMovieById(id:number):Observable<Movies>{
    return this.http.get<Movies>(`${this.url}/${id}`)
  }
  getRatingByMovieId(id:number):Observable<Ratings[]>{
    return this.http.get<Ratings[]>(`${this.ratingUrl}/${id}`)
  }

  updateMovie(imageData: any,movieId: number){
    console.log(imageData,"before");
    let data = {
      imageData: imageData,
      movieId: movieId
    }
    const headers= new HttpHeaders({Authorization: localStorage.getItem('AuthJwtToken')})
    const requestOptions={headers:headers}
    
    return this.http.put<any>(this.url, imageData, requestOptions  );
  }

  delMovie(id:number){
    const headers= new HttpHeaders({Authorization: localStorage.getItem('AuthJwtToken')})
    const requestOptions={headers:headers}
    return this.http.delete<Movies>(`${this.url}/${id}`, requestOptions)
  }
}
