import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { UserProfile } from '../models';
import { UserService } from '../services';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styles: [
  ]
})
export class HeaderComponent implements OnInit {
  loggedIn = false;
  userProfile: UserProfile;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userProfile = this.userService.getUserProfile();
    console.log(this.userProfile);
  }

  login() {

  }

  logout() {

  }

  getUserName() {

  }

  getCurrentDate() {
    return moment().format("D MMM YYYY");
  }

}