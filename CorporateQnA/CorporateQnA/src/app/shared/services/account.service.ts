import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { RegisterUser, UserProfile } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44399/api';

  register(user: RegisterUser) {
    console.log(user);
    console.log("Hello nshivam");
    return this.http.post(this.BaseURI + '/account/register', user);
  }
  login(loginData) {
    return this.http.post(this.BaseURI + '/account/login', loginData);
  }

  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payLoad.role;
    allowedRoles.forEach(element => {
      if (userRole == element) {
        isMatch = true;
      }
    });
    return isMatch;
  }
  getRole() {
    return JSON.parse(window.atob(localStorage.getItem('token').split('.')[1])).role;
  }
  getUserId() {
    return JSON.parse(window.atob(localStorage.getItem('token').split('.')[1])).UserID;
  }
  getUserProfile() {
    var tokenData = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    console.log("hello" + tokenData);
    return new UserProfile(tokenData.UserId, tokenData.UserName, tokenData.UserImage);
  }
  isUserLoggedIn() {
    if (localStorage.getItem('token'))
      return true;

    return false;
  }
}
