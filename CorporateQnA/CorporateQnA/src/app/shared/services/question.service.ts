import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  constructor(private http: HttpClient) { }
  readonly baseUrl = 'https://localhost:44399/api/question';

  getAllQuestions(): Observable<any> {
    return this.http.get(this.baseUrl + '/details');
  }

  getQuestionByUserId(userId: number) {
    return this.http.get(this.baseUrl + `/${userId}/all`);
  }
}
