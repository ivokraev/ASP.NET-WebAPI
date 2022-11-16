import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppUsers } from '../models/AppUser.model';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  getUsers(): Observable<AppUsers>{
    return this.http.get<AppUsers>('https://localhost:7183/api/users');
  }
}
