import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WatchLaterService {

  Url="https://localhost:7126/api/WatchLater"
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
    return this.http.delete(`${this.Url}/${mId}`,requestOptions)
  }


}
