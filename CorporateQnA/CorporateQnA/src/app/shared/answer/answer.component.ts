import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import * as moment from 'moment';
import { AnswerDetails } from '../models/answer-details.model';
import { AnswerService, QuestionService } from '../services';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styles: []
})
export class AnswerComponent implements OnInit {
  @Input() answer: AnswerDetails;
  @Input() isQuestionResolved: boolean = false;
  @Output() setQuestionResolvedState: EventEmitter<{ answerId: number, questionId: number, resolveState: boolean }> = new EventEmitter();
  //ICONS
  thumbsUp = faThumbsUp
  thumbsDown = faThumbsDown;

  timeAgo: string;

  user: any
  setAsSolution: FormGroup;

  constructor(private answerService: AnswerService, private questionService: QuestionService) { }

  ngOnInit() {

  }
  createAnswerActivity(x) {

  }
}

