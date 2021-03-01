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
  getUserProfile() {
    var tokenData = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    return new UserProfile(tokenData.UserId, tokenData.UserName, tokenData.UserImage);
  }
}
