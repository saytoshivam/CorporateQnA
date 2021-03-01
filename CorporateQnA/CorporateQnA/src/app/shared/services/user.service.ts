import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserProfile } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }
  readonly baseUrl = 'https://localhost:44399/api/user';

  getAllUsers(): Observable<any> {
    return this.http.get(this.baseUrl + '/details');
  }
}
