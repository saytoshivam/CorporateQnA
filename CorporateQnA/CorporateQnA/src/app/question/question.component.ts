import { Component, OnInit } from '@angular/core';
import { QuestionDetails } from '../shared/models';
import { QuestionService } from '../shared/services';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styles: [
  ]
})
export class QuestionComponent implements OnInit {

  quesionDetailsList: QuestionDetails[];
  constructor(private questionService: QuestionService) { }

  ngOnInit(): void {
    this.questionService.getAllQuestions().subscribe(res => {
      this.quesionDetailsList = <QuestionDetails[]>res;
      console.log(this.quesionDetailsList);

    })
  }

}
