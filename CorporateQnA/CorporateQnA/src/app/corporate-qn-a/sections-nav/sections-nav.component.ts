import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../shared/services';

@Component({
  selector: 'app-sections-nav',
  templateUrl: './sections-nav.component.html',
  styles: []
})
export class SectionsNavComponent implements OnInit {

  isLoggedIn = false;
  constructor(private accountService: AccountService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.isLoggedIn = this.accountService.isUserLoggedIn();
  }

  getRoute() {
    return this.router.url;
  }

}