import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Answer } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AnswerService {
  constructor(private http: HttpClient) { }
  readonly baseUrl = 'https://localhost:44399/api/answer';


  postAnswer(answer: Answer): Observable<any> {
    return this.http.post(`${this.baseUrl}/add`, answer);
  }

  getAnswersForQuestion(userId: number, questionId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${userId}/details/${questionId}`);
  }
  likeAnswer(userId, answerId) {
    return this.http.post(`${this.baseUrl}/${userId}/like/${answerId}`, '');
  }
  dislikeAnswer(userId, answerId) {
    return this.http.post(`${this.baseUrl}/${userId}/like/${answerId}`, '');
  }
  markAsSolution(userId, answerId) {
    return this.http.post(`${this.baseUrl}/${userId}/markSolution/${answerId}`, '');
  }
}
