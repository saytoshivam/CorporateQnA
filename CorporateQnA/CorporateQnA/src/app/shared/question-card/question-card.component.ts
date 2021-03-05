import { Component, Input, OnInit } from '@angular/core';
import * as moment from 'moment';
import { chevronUpIcon, eyeIcon } from '../constants';

import { QuestionDetails } from '../models';
import { AccountService, QuestionService } from '../services';

@Component({
  selector: 'app-question-card',
  templateUrl: './question-card.component.html',
  styles: []
})
export class QuestionCardComponent implements OnInit {
  @Input() question: QuestionDetails
  //Icons
  faChevronUp = chevronUpIcon
  faEye = eyeIcon

  loggedInUserId: number
  timeAgo = ""

  constructor(private questionService: QuestionService, private accountService: AccountService) { }

  ngOnInit() {
    this.loggedInUserId = this.accountService.getUserId();

    this.timeAgo = moment(this.question.askedOn).fromNow()
  }
  upvoteQuestion() {
    this.questionService.upvoteQuestion(this.loggedInUserId, this.question.id).subscribe(isupvoted => {
      if (isupvoted)
        this.question.upVoteCount++;

    });
  }
}
