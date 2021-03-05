import { Component, Input, OnInit } from '@angular/core';

import { thumbsUpIcon, thumbsDownIcon } from '../../../shared/constants';
import { UserDetails } from 'src/app/shared/models';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styles: []
})
export class UserCardComponent implements OnInit {

  @Input() userDetail: UserDetails;

  thumbsUp = thumbsUpIcon
  thumbsDown = thumbsDownIcon
  constructor() { }

  ngOnInit(): void {
  }

}
