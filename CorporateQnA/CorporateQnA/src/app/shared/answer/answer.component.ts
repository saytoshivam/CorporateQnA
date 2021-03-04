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

  constructor(private answerService: AnswerService, private accountService: AccountService) { }

  ngOnInit() {
    this.loggedInUserId = this.accountService.getUserId();
  }
  createAnswerActivity(x) {

  }
  likeAnswer() {
    this.answerService.likeAnswer(this.loggedInUserId, this.answer.id).subscribe(isLiked => {
      if (isLiked)
        this.answer.totalLikes++;
      else
        this.answer.totalLikes--;
    });
  }
  dislikeAnswer() {
    this.answerService.dislikeAnswer(this.loggedInUserId, this.answer.id).subscribe(isDisliked => {
      if (isDisliked)
        this.answer.totalDislikes++;
      else
        this.answer.totalDislikes--;
    })
  }
}

