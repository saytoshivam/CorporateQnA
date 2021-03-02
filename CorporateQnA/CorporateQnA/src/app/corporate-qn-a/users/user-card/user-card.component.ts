import { Component, Input, OnInit } from '@angular/core';
import { UserDetails } from 'src/app/shared/models';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styles: []
})
export class UserCardComponent implements OnInit {

  @Input() userDetail : UserDetails;

   // thumbsUp = faThumbsUp
   // thumbsDown = faThumbsDown
  constructor() { }

  ngOnInit(): void {
  }

}
