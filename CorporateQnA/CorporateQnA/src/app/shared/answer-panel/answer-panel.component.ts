import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { faCompressAlt, faExpandAlt } from '@fortawesome/free-solid-svg-icons';
import * as moment from 'moment';

import { compressAltIcon, expandAltIcon } from '../../shared/constants';
import { QuestionDetails, Answer, AnswerDetails } from '../models';
import { AccountService, AnswerService, QuestionService } from '../services';

@Component({
  selector: 'app-answer-panel',
  templateUrl: './answer-panel.component.html',
  styles: []
})
export class AnswerPanelComponent implements OnInit {
  @Input() question: QuestionDetails

  //ICONS
  faExpandAlt = expandAltIcon
  faCompressAlt = compressAltIcon

  //FORM
  newAnswer: FormGroup;

  questionTimeAgo: string

  //Initially not set
  solutionAnswerId: number = -1;
  userData: any

  answerCount = 0;
  answers: AnswerDetails[] = []

  toggleFlyoutEditor = false

  //logged in user Details
  loggedInUserId: number;
  loggedInUserFullName: string;

  constructor(private questionService: QuestionService, private answerService: AnswerService, private accountService: AccountService) { }

  ngOnInit() {

    this.loggedInUserId = this.accountService.getUserId();
    console.log("init log", this.loggedInUserId)
    this.loggedInUserFullName = this.accountService.getUserFullName();
    this.newAnswer = new FormGroup({
      content: new FormControl("", [Validators.required, this.editorValidator()]),
    })

    this.answerService.getAnswersForQuestion(this.loggedInUserId, this.question.id).subscribe(answers => {
      this.answers = answers;
      this.answerCount = answers.length
    })

    this.questionTimeAgo = moment(this.question.askedOn).fromNow()
  }

  ngOnChanges(changes: SimpleChanges) {

    if (this.loggedInUserId)
      this.answerService.getAnswersForQuestion(this.loggedInUserId, this.question.id).subscribe(answers => {
        this.answers = answers;
        this.answerCount = answers.length
      })

  }

  removeTags(str) {
    if ((str === null) || (str === ''))
      return false;
    else
      str = str.toString();

    // Regular expression to identify HTML tags in  
    // the input string. Replacing the identified  
    // HTML tag with a null string. 
    return str.replace(/(<([^>]+)>)/ig, '');
  }

  postAnswer() {
    let answeredBy = this.loggedInUserId
    let questionId = this.question.id;
    let questionsAnswer = this.removeTags(this.newAnswer.get('content').value);

    let answerModel: Answer = new Answer({ answeredBy, questionId, questionsAnswer })

    this.answerService.postAnswer(answerModel).subscribe(answerId => {

      let answerDetail: AnswerDetails = new AnswerDetails({
        id: answerId,
        totalDislikes: 0,
        totalLikes: 0,
        answer: questionsAnswer,
        answeredOn: moment(),
        fullName: this.loggedInUserFullName,
        askedBy: this.question.askedBy,
        isBestSolution: false
      })

      this.answers.push(answerDetail);
      this.newAnswer.get('content').patchValue("")
      this.answerCount++;
    })
  }

  editorValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      let empty = this.removeTags(control.value).length == 0

      return empty ? { "empty": "Empty content" } : null;
    };
  }

  questionEvent(event: { answerId: number }) {
    this.answerService.markAsSolution(this.loggedInUserId, event.answerId).subscribe(value => {
      this.question.isResolved = <boolean>value
    })
  }

  reportQuestion() {
    this.questionService.reportQuestion(this.loggedInUserId, this.question.id).subscribe();
  }
}
