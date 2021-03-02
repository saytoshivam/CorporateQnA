import { Component, Input, OnInit } from '@angular/core';
import { QuestionDetails } from '../models';

@Component({
  selector: 'app-question-card',
  templateUrl: './question-card.component.html',
  styles: []
})
export class QuestionCardComponent implements OnInit {
  @Input() question: QuestionDetails
  constructor() { }

  ngOnInit(): void {
  }

}
