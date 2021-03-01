import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-sections-nav',
  templateUrl: './sections-nav.component.html',
  styles: []
})
export class SectionsNavComponent implements OnInit {

  constructor(private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() { }

  getRoute() {
    return this.router.url;
  }

}