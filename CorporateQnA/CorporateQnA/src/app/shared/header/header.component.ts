import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { UserProfile } from '../models';
import { AccountService, UserService } from '../services';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styles: [
  ]
})
export class HeaderComponent implements OnInit {
  userProfile: UserProfile;

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit() {
    this.userProfile = this.accountService.getUserProfile();
    console.log(this.userProfile);
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/account/login']);
  }

  getCurrentDate() {
    return moment().format("D MMM YYYY");
  }
}