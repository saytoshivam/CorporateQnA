import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';

import { QuestionDetails, AnswerDetails } from '../models';
import { AccountService, AnswerService } from '../services';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styles: []
})
export class AnswerComponent implements OnInit {
  @Input() answer: AnswerDetails;
  @Input() question: QuestionDetails;
  @Output() setQuestionResolvedState: EventEmitter<{ answerId: number }> = new EventEmitter();
  //ICONS
  thumbsUp = faThumbsUp
  thumbsDown = faThumbsDown;

  timeAgo: string;

  loggedInUserId: number;
  setAsSolution: FormGroup;

  constructor(private answerService: AnswerService, private accountService: AccountService) { }

  ngOnInit() {
    this.loggedInUserId = this.accountService.getUserId();
    this.setAsSolution = new FormGroup({
      isBestSolution: new FormControl()
    })

    this.setAsSolution.get('isBestSolution').patchValue(this.answer.isBestSolution)
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

  markAsSolution() {
    this.setQuestionResolvedState.emit({ answerId: this.answer.id });
  }
}

