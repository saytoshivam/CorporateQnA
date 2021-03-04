import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import * as moment from 'moment';
import { QuestionDetails } from '../models';
import { AnswerDetails } from '../models/answer-details.model';
import { AccountService, AnswerService, QuestionService } from '../services';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styles: []
})
export class AnswerComponent implements OnInit {
  @Input() answer: AnswerDetails;
  @Input() question: QuestionDetails;
  @Output() setQuestionResolvedState: EventEmitter<{ answerId: number, questionId: number, resolveState: boolean }> = new EventEmitter();
  //ICONS
  thumbsUp = faThumbsUp
  thumbsDown = faThumbsDown;

  timeAgo: string;

  loggedInUserId: number;
  setAsSolution: FormGroup;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.loggedInUserId = this.accountService.getUserId();
  }
  createAnswerActivity(x) {

  }
}

