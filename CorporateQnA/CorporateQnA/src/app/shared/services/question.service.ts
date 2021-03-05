import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Question } from '../models';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  constructor(private http: HttpClient) { }
  readonly baseUrl = 'api/question';

  reportQuestion(userId, questionId) {
    return this.http.post(this.baseUrl + `/${userId}/report/${questionId}`, '');
  }
  postQuestion(question: Question): Observable<any> {
    return this.http.post(`${this.baseUrl}`, question);
  }

  getAllQuestions(): Observable<any> {
    return this.http.get(this.baseUrl + '/details');
  }

  getQuestionsByUserId(userId: number) {
    return this.http.get(this.baseUrl + `/${userId}/all`);
  }

  upvoteQuestion(userId, questionId) {
    return this.http.post(this.baseUrl + `/${userId}/upvote/${questionId}`, '');
  }
}
