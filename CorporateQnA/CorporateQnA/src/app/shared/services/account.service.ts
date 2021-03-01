import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { UserProfile } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44399/api';

  register(formModel) {
    var body = {
      userName: formModel.value.userName,
      email: formModel.value.email,
      fullName: formModel.value.fullName,
      password: formModel.value.passwords.password
    };
    return this.http.post(this.BaseURI + '/account/register', body);
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
    return new UserProfile(tokenData.UserId, tokenData.UserName, tokenData.UserImage);
  }
  isUserLoggedIn() {
    if (localStorage.getItem('token'))
      return true;

    return false;
  }
}
