import { Component, Input, OnInit } from '@angular/core';
import * as moment from 'moment';
import { QuestionDetails } from '../models';
import { QuestionService } from '../services';

@Component({
  selector: 'app-question-card',
  templateUrl: './question-card.component.html',
  styles: []
})
export class QuestionCardComponent implements OnInit {
  @Input() question: QuestionDetails
  //Icons
  // faChevronUp = faChevronUp
  // faEye = faEye

  user: any;
  timeAgo = ""

  constructor(private questionService: QuestionService) { }

  ngOnInit() {
    console.log("in question card" + this.question);
    this.user = 2;//logged in usser id from token

    this.timeAgo = moment(this.question.askedOn).fromNow()
  }
  viewAnswers() { }
  upvoteQuestion() { }
}
