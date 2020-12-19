import { HttpClient } from '@angular/common/http';
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
}