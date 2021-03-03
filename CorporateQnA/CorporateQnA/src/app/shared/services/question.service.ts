import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Question } from '../models/question.model';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  constructor(private http: HttpClient) { }
  readonly baseUrl = 'https://localhost:44399/api/question';

  postQuestion(question: Question): Observable<any> {
    return this.http.post(`${this.baseUrl}/add`, question);
  }

  getAllQuestions(): Observable<any> {
    return this.http.get(this.baseUrl + '/details');
  }

  getQuestionsByUserId(userId: number) {
    return this.http.get(this.baseUrl + `/${userId}/all`);
  }

  searchQuestion(x) {
    return x;
  }
}
