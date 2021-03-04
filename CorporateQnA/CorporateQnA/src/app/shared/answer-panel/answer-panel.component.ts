import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { faCompressAlt, faExpandAlt } from '@fortawesome/free-solid-svg-icons';
import * as moment from 'moment';
import { QuestionDetails } from '../models';
import { AnswerDetails } from '../models/answer-details.model';
import { Answer } from '../models/answer.model';
import { GetAnswersModel } from '../models/get-answers.model';
import { AccountService, AnswerService } from '../services';

@Component({
  selector: 'app-answer-panel',
  templateUrl: './answer-panel.component.html',
  styles: []
})
export class AnswerPanelComponent implements OnInit {
  @Input() question: QuestionDetails

  //ICONS
  faExpandAlt = faExpandAlt
  faCompressAlt = faCompressAlt;

  //FORM
  newAnswer: FormGroup;

  questionTimeAgo: string

  //Initially not set
  solutionAnswerId: number = -1;
  userData: any

  answerCount = 0;
  answers: AnswerDetails[] = []
  fetchquery: GetAnswersModel

  toggleFlyoutEditor = false
  loggedInUserId: number;

  constructor(private answerService: AnswerService, private accountService: AccountService) { }

  ngOnInit() {

    this.loggedInUserId = this.accountService.getUserId();
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

    if (this.userData != null) {
      this.answerService.getAnswersForQuestion(2, this.question.id).subscribe(values => {
        this.answers = values;
        this.answerCount = values.length
      })
    }
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
    console.log("in answer panel");
    let answeredBy = this.loggedInUserId
    let questionId = this.question.id;
    let questionsAnswer = this.removeTags(this.newAnswer.get('content').value);

    let answerModel: Answer = new Answer({ answeredBy, questionId, questionsAnswer })

    this.answerService.postAnswer(answerModel).subscribe(answerId => {

      let answerDetail: AnswerDetails = new AnswerDetails({
        answerId,
        likeCount: 0,
        dislikeCount: 0,
        questionsAnswer,
        answeredBy: this.loggedInUserId,
        answeredOn: moment(),
        questionId,
        fullName: this.userData['name'],
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

  questionEvent(event: { questionId: number, resolveState: boolean, answerId: number }) {
    this.question.isResolved = event.resolveState
    this.solutionAnswerId = event.answerId
  }

}
