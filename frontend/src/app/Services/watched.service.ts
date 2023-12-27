import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WatchedService {

  Url="https://localhost:7126/api/Watched"
  token= localStorage.getItem('AuthJwtToken')


  constructor(private http:HttpClient) { }

  addToWL(mId){
    const headers= new HttpHeaders({'Content-Type':'application/json',Authorization:this.token})
    const requestOptions={headers:headers}
    return this.http.post(this.Url, mId, requestOptions)
  }

  getWl(){
    const headers= new HttpHeaders({'Content-Type':'application/json',Authorization:this.token})
    const requestOptions={headers:headers}
    return this.http.get(this.Url,  requestOptions)
  }

  delWl(mId){
    const headers= new HttpHeaders({'Content-Type':'application/json',Authorization:this.token})
    const requestOptions={headers:headers}
    return this.http.post(this.Url, mId, requestOptions)
  }}
