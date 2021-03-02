import { Component, Input, OnInit } from '@angular/core';
import { QuestionDetails } from '../models';

@Component({
  selector: 'app-answer-panel',
  templateUrl: './answer-panel.component.html',
  styles: [
  ]
})
export class AnswerPanelComponent implements OnInit {
  @Input() question: QuestionDetails
  constructor() { }

  ngOnInit(): void {
  }

}
