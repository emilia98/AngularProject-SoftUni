import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  baseUrl = "http://localhost:5000/property/";
  
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.baseUrl + 'all')
        .pipe((response: any) => {
           return response;
        })
  }
  
  getMine() {
    let headers = this.generateAuthHeaders()
    return this.http.get(this.baseUrl + 'my', { headers: headers})
        .pipe((response: any) => {
            return response;
        });
  }

  private generateAuthHeaders() {
    let userItem = localStorage.getItem('user');
    let token = null;
    if (userItem) {
      token = JSON.parse(userItem).data.token;
    }
   
    var headers = new HttpHeaders(
      { 'Authorization': `Bearer ${token}` }
    )

    return headers;
  }
}